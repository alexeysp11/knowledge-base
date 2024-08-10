# Кэширование в хранимых процедурах 

[English](caching.md) | [Русский](caching.ru.md)

## Исходные данные

- Хранимая процедура в БД:
    - предполагаемое название: `f_get_order_report`.
    - функционал: обновляет строчки заказов потребителей и возвращает **заказ потребителя/поставщика**.
    - входные параметры: 
        - `a_mutual_agreement_igt` - признак ВГО (внутригрупповой операции) во взаимном соглашении.
        - `a_soi_price_min` - минимальная цена строчки заказа поставщика.
        - `a_bill_start_date` - начальная дата фильтрации счёта.
        - `a_bill_end_date` - конечная дата фильтрации счёта.
    - выходные параметры: 
        - `document_id` - ИД из таблиц `orders` и `suppl_order`.
        - `f_order_direction` - признак поставщика/потребителя.
- Таблицы: 
    - `mutual_agreement` и `mutual_agreement_catalogue` - взаимные соглашения (например, о взаиморасчетах: акты сверки и реестры актов сверки).
    - `customer` - потребители (физлица и юрлица).
    - `orders` и `order_item` - заказы потребителей и строчки заказов потребителей.
    - `bill` - счета потребителей.
    - `supplier` - поставщики.
    - `suppl_order` и `suppl_order_item` - заказы поставщиков и строчки заказов поставщиков.
    - `suppl_bill` - счета поставщиков.
    - `data_configs` - общие настройки конфигурации для использования внутри БД (условия предоставления скидок/наценок потребителям, внутренних правил интеграции с финансовыми организациями).
- Временные таблицы: 
    - `tmp_id1: id1`.

## Описание процедуры

- **первая фильтрация** на основании входных значений и параметров/условий фильтрации из `data_configs`.
    - Таблицы: 
        - взаимные соглашения, 
        - каталог взаимных соглашений,
        - заказы потребителя и строчки заказа потребителя,
        - заказы поставщика и строчки заказа поставщика.
    - **Заполняет временную таблицу с одним полем** - `tmp_id1`.
    - Пример кода: 
```SQL
l_func_name := 'f_get_order_report';
l_dc_discount_key = 'discount';

insert into tmp_id1 (id)
select distinct ma.mutual_agreement_id
from mutual_agreement ma 
left join mutual_agreement_catalogue mac on ma.mutual_agreement_id = mac.mutual_agreement_id
left join orders o on ma.order_id = o.order_id 
left join order_item oi on o.order_id = oi.order_id
left join suppl_order so on ma.suppl_order_id = so.suppl_order_id 
left join suppl_order_item soi on so.suppl_order_id = soi.suppl_order_id 
left join data_configs dc_customer on dc_customer.customer_id = o.customer_id
left join data_configs dc_suppl on dc_suppl.supplier_id = so.supplier_id
...
left join customer c on o.customer_id = c.customer_id
left join supplier s on so.supplier_id = s.supplier_id
where 1 = 1 
    and mac.f_igt = coalesce(a_mutual_agreement_igt, 0)
    and dc_customer.func_name = l_func_name
    and dc_suppl.func_name = l_func_name
    and (
        (dc_customer.key = l_dc_discount_key and dc_customer.value_num <= 15) 
        or (dc_suppl.key = l_dc_discount_key and dc_suppl.value_num >= 5))
    and soi.price >= coalesce(a_soi_price_min, 0)
    ...
    and ma.number_parties in (2,3,4,5)
;
```
- обновляет `order_item` по связке с взаимными соглашениями (**здесь используется временная таблица с одним полем** - `tmp_id1`):
```SQL
l_datetime := now();

update order_item oi 
set 
    oi.date_in_claim = l_datetime,
    oi.notes = coalesce(oi.notes || '. ', '') || 'Order updated: ' || to_char(l_datetime, 'dd.MM.yyyy')
where oi.order_id in (
    select ma.order_id
    from tmp_id1 t 
    inner join mutual_agreement ma on t.id1 = ma.mutual_agreement_id
);
```
- **вторая фильтрация** на основании параметров/условий фильтрации из `data_configs` (**здесь также используется временная таблица с одним полем** - `tmp_id1`):
```SQL
return query 
    select document_id, f_order_direction
    from (
        select 
            orders_id as document_id, 
            1 as f_order_direction
        from orders o 
        where o.order_id in (
            select ma.order_id
            from tmp_id1 t 
            inner join mutual_agreement ma on t.id1 = ma.mutual_agreement_id
            left join bill b on ma.mutual_agreement_id = b.mutual_agreement_id 
            left join customer c on b.customer_id = c.customer_id
            left join data_configs dc on dc.func_name = l_func_name
            where 1 = 1
                and (dc.key = 'bill_total_price' and b.total_price <= dc.value_num)
                and c.confirmed = 1
                and (b.create_date between a_bill_start_date and a_bill_end_date)
        )
        union 
        select 
            so.suppl_order_id as document_id, 
            2 as f_order_direction
        from suppl_order so 
        where so.suppl_order_id in (
            select ma.suppl_order_id
            from tmp_id1 t 
            inner join mutual_agreement ma on t.id1 = ma.mutual_agreement_id
            left join suppl_bill sb on ma.mutual_agreement_id = sb.mutual_agreement_id 
            left join supplier s on s.supplier_id = sb.supplier_id
            left join data_configs dc on dc.func_name = l_func_name
            where 1 = 1
                and (dc.key = 'suppl_bill_total_price' and sb.total_price <= dc.value_num)
                and s.confirmed = 1
                and (sb.create_date between a_bill_start_date and a_bill_end_date)
        )
    ) tt 
;
```

### Обоснование использования временных таблиц

**В общем смысле**, временные таблицы могут быть использованы в следующих случаях:
- хранение промежуточных расчетов внутри сложных последовательностей CREATE или UPDATE запросов (например, создание summary tables для OLAP database) - [источник](https://stackoverflow.com/questions/4976585/what-are-the-use-cases-for-temporary-tables-in-sql-database-systems).
- увеличения производительности SELECT-запросов (пример: в запросе примерно 20 джоинов -> запрос выполняется медленно -> можно сджоинить таблицы с наименьшим количеством записей и сохранить полученные записи во временной таблице -> сджоинить временную таблицу с остальными таблицами).
- иногда временные таблицы могут выступать в роли [промежуточных таблиц](https://en.wikipedia.org/wiki/Associative_entity):

![Mapping_table_concept](https://upload.wikimedia.org/wikipedia/commons/d/d7/Mapping_table_concept.png)

О том, что такое промежуточная таблица, и зачем они используются, можно прочитать по следующим ссылкам: 
- [Oracle documentaion: Define Intermediate Table Joins](https://docs.oracle.com/en/cloud/saas/b2c-service/22d/famug/t-Define-intermediate-table-joins-ah1136329.html#ah1136329)
- [MySQL Many to Many with additional data in intermediary table](https://dba.stackexchange.com/questions/271665/mysql-many-to-many-with-additional-data-in-intermediary-table).
- [Intermediate SQL Table : What for?](https://stackoverflow.com/questions/32172989/intermediate-sql-table-what-for).
- [Wikipedia: Associative entity](https://en.wikipedia.org/wiki/Associative_entity): в статье указано, что термины "таблица ассоциаций", "промежуточная таблица" и "таблица пересечений" могут быть взаимозаменяемы (также перечислено несколько других наименований).

**В контексте процедуры** `f_get_order_report`:
- Временная таблица `tmp_id1` используется для кэширования данных (данные из этой таблицы используются дважды: для обновления `order_item` и для финальной фильтрации при `return`).
- `tmp_id1` - хранит результат первой фильтрации: 
    - `id1`- ИД из таблицы `mutual_agreement` после первой фильтрации.

## Проблема изоляции временных данных

В ситуациях, когда есть необходимость в стандартизации временных таблиц (у такой необходимости могут быть свои определенные причины), то целесообразно сделать следующее:
- Написать server-side модуль на `C`, перехватывающий **logon** или **начало сессии**.
Например, [по данной ссылке](https://stackoverflow.com/questions/48104368/postgresql-trigger-on-user-logon) есть объяснение, как  перехватить logon пользователя с помощью [ClientAuthentication_hook](https://github.com/postgres/postgres/blob/master/src/include/libpq/auth.h) для PostgreSQL ([оффициальная документация о hooks в PostgreSQL в формате PDF](https://wiki.postgresql.org/images/e/e3/Hooks_in_postgresql.pdf)).
- При перехвате события, запускать скрипт создания всех необходимых временных таблиц.

Это позволит создать иллюзию того, что временные таблицы являются общими для всех пользователей/сессий, работающих с БД (таким образом, программист использует *только стандартизированные временные таблицы*). 
Но при этом такой подход также изолирует сами данные пользователей/сессий, чтобы избежать коллизий (например, когда первый записывает, второй удаляет данные, первый безуспешно читает данные). 

Альтернативное, и более простое, решение может заключаться в том, чтобы создавать все временные таблицы, необходимые для выполнения конкретной процедуры, лишь по мере вызова самой процедуры.

Согласно документации [PostgreSQL docs: CREATE TABLE](https://www.postgresql.org/docs/current/sql-createtable.html), **временная таблица будет существовать только внутри сессии/транзакции**, поэтому в данном случае нет смысла беспокоиться об [изоляции](https://en.wikipedia.org/wiki/Isolation_(database_systems)) (до тех пор, пока вы не будете вызывать функции, потенциально использующих те же самые временные таблицы):
> If specified, the table is created as a temporary table. Temporary tables are automatically dropped at the end of a session, or optionally at the end of the current transaction (see ON COMMIT below). The default search_path includes the temporary schema first and so identically named existing permanent tables are not chosen for new plans while the temporary table exists, unless they are referenced with schema-qualified names. Any indexes created on a temporary table are automatically temporary as well.

Скрипт для создания `tmp_id1`:
```SQL
CREATE TEMPORARY TABLE tmp_id1 (
    id1 integer
);
```

## Альтернативный пример использования кэширования 

Если в некоторой (другой) процедуре используется часть кода, которая долго вычисляет какие-то метрики по данным, которые согласно бизнес-логике не должны меняться в будущем (например, договоры, по которым проводился процесс согласования), то эти метрики можно вынести в отдельную таблицу, которая кэширует эти метрики, и обновлять эту таблицу один раз в день и/или по мере изменения данных в таблице договоров. 

<!--
Есть SQL-запрос вида:
```SQL
with recursive 
item as (
    select * 
    from erp_spec es
    left join price_establish pe on pe.erp_spec_id = es.erp_spec_id
), 
lowlvl_spec as (
    select * 
    from low_level_spec lls
    where 1 = 1
    union 
    select * 
    from low_level_spec lls
    left join lowlvl_spec p on p.id = lls.parent_id
    where 1 = 1
), 
max_price_spec as (
    // 
),
price_spec as (
    // 
)
select t.* 
from (
    // 
) t 
```

Если данный запрос становится слишком громоздким и тяжёлым для поддерживания, то его можно преобразовать в функцию следующего вида:
```SQL
create or replace function()
begin
end;
```

-->

## Ограничения

При работе с запросами, использующими временные таблицы или таблицы-заглушки, могут возникнуть следующие проблемы:
- Замедление процессов из-за создания и удаления временных таблиц.
- Неэффективное использование памяти из-за большого объема данных во временных таблицах.
- Проблемы с доступом к данным из разных сессий.

## Выводы 

Использование временных таблиц обусловлено не желанием усложнить процессы или "**просто переложить данные из одной таблицы в другую, чтобы потом их прочитать**", а намерением снизить нагрузку на оперативную память и уменьшить количество повторов aka идентичных операций (что особенно актуально, когда нужно по одним и тем же данным выполнить несколько действий). 

Также не стоит путать временные таблицы с промежуточными таблицами, поскольку это разные концепты. 
Однако при кэшировании, временная таблица может стать промежуточной (таблицей ассоциаций/пересечений).
