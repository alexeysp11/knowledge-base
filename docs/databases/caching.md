# Caching in stored procedures

Read this in other languages: [English](caching.md), [Russian/Русский](caching.ru.md). 

## Initial data

- Stored procedure in the database:
     - suggested name: `f_get_order_report`.
     - functionality: updates customer order lines and returns **consumer/supplier order**.
     - input parameters:
         - `a_mutual_agreement_igt` - sign of IGT (intra-group transaction) in a mutual agreement.
         - `a_soi_price_min` - minimum price of a supplier's order line.
         - `a_bill_start_date` - bill filtering start date.
         - `a_bill_end_date` - bill filtering end date.
     - output parameters:
         - `document_id` - ID from `orders` and `suppl_order` tables.
         - `f_order_direction` - supplier/consumer attribute.
- Tables:
     - `mutual_agreement` and `mutual_agreement_catalogue` - mutual agreements (for example, on mutual settlements: reconciliation acts and registries of reconciliation acts).
     - `customer` - consumers (individuals and legal entities).
     - `orders` and `order_item` - consumer orders and consumer order lines.
     - `bill` - bills of consumers.
     - `supplier` - suppliers.
     - `suppl_order` and `suppl_order_item` - supplier orders and supplier order lines.
     - `suppl_bill` - supplier bills.
     - `data_configs` - general configuration settings for use inside the database (conditions for providing discounts/markups to consumers, internal rules for integration with financial institutions).
- Temporary tables:
     - `tmp_id1: id1`.

## Procedure description

- **first filtering** based on input values and filtering parameters/conditions from `data_configs`.
     - Tables:
         - mutual agreements
         - catalog of mutual agreements,
         - consumer orders and consumer order lines,
         - supplier orders and supplier order lines.
     - **Fills a temporary table with one field** - `tmp_id1`.
     - Sample code:
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
- updates `order_item` knowing IDs of the filtered mutual agreements (**this uses a temporary table with one field** - `tmp_id1`):
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
- **Second filtering** based on parameters/filtering conditions from `data_configs` (**this also uses a temporary table with one field** - `tmp_id1`):
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

### Why to use temporary tables

**In a general sense**, temporary tables can be used in the following cases:
- storing intermediate calculations inside complex sequences of CREATE or UPDATE queries (for example, creating summary tables for an OLAP database) - [source](https://stackoverflow.com/questions/4976585/what-are-the-use-cases-for-temporary-tables-in-sql-database-systems).
- increase the performance of SELECT queries (example: there are about 20 joins in the query -> the query is slow -> you can join the tables with the least number of records and save the resulting records in a temporary table -> join the temporary table with the rest of the tables).
- sometimes temporary tables can act as [intermediate tables](https://en.wikipedia.org/wiki/Associative_entity):

![Mapping_table_concept](https://upload.wikimedia.org/wikipedia/commons/d/d7/Mapping_table_concept.png)

You can read about what an intermediate table is and why they are used at the following links:
- [Oracle documentaion: Define Intermediate Table Joins](https://docs.oracle.com/en/cloud/saas/b2c-service/22d/famug/t-Define-intermediate-table-joins-ah1136329.html#ah1136329)
- [MySQL Many to Many with additional data in intermediary table](https://dba.stackexchange.com/questions/271665/mysql-many-to-many-with-additional-data-in-intermediary-table).
- [Intermediate SQL Table : What for?](https://stackoverflow.com/questions/32172989/intermediate-sql-table-what-for).
- [Wikipedia: Associative entity](https://en.wikipedia.org/wiki/Associative_entity): The article states that the terms "association table", "intermediate table", and "intersection table" can be used interchangeably (also lists several other names).

**In the context of procedure** `f_get_order_report`:
- Temporary table `tmp_id1` is used for data caching (data from this table is used twice: for updating `order_item` and for final filtering on `return`).
- `tmp_id1` - stores the result of the first filtering:
     - `id1` - ID from the `mutual_agreement` table after the first filtering.

## Temporary data isolation problem

In situations where there is a need to standardize temporary tables (the need may have its own specific reasons), it is advisable to do the following:
- Write a server-side module in `C` that hooks **logon** or **session start**.
For example, [following this link](https://stackoverflow.com/questions/48104368/postgresql-trigger-on-user-logon) there is an explanation of how to intercept user logon using [ClientAuthentication_hook](https://github.com/postgres/postgres/blob/master/src/include/libpq/auth.h) for PostgreSQL ([Official PostgreSQL hooks PDF documentation](https://wiki.postgresql.org/images/e/e3/Hooks_in_postgresql.pdf)).
- When intercepting an event, run the script for creating all necessary temporary tables.

This will create the illusion that temporary tables are shared by all users/sessions working with the database (thus, the programmer uses *only standardized temporary tables*).
But at the same time, this approach also isolates the user/session data itself in order to avoid collisions (for example, when the first writes, the second deletes data, the first unsuccessfully reads the data).

An alternative, and simpler, solution might be to create all the temporary tables needed to execute a particular procedure only as the procedure itself is called.

According to the documentation [PostgreSQL docs: CREATE TABLE](https://www.postgresql.org/docs/current/sql-createtable.html), **temporary table will only exist within a session/transaction**, so in this case you don't have to worry about [isolation](https://en.wikipedia.org/wiki/Isolation_(database_systems)) (as long as you don't call functions potentially using the same temporary tables):

> If specified, the table is created as a temporary table. Temporary tables are automatically dropped at the end of a session, or optionally at the end of the current transaction (see ON COMMIT below). The default search_path includes the temporary schema first and so identically named existing permanent tables are not chosen for new plans while the temporary table exists, unless they are referenced with schema-qualified names. Any indexes created on a temporary table are automatically temporary as well.

The script for creating `tmp_id1`:
```SQL
CREATE TEMPORARY TABLE tmp_id1 (
    id1 integer
);
```

## Alternative example of using caching

If some (other) procedure uses a part of the code that takes a long time to calculate some metrics based on data that, according to business logic, should not change in the future (for example, contracts for which the approval process was carried out), then these metrics can be moved to a separate a table that caches these metrics, and update this table once a day and/or as data changes in the contract table.

## Conclusions

The use of temporary tables is not due to the desire to complicate processes or "**just transfer data from one table to another in order to read them later**", but by the intention to reduce the load on RAM and reduce the number of repetitions aka identical operations (which is especially important when you need to perform several actions on the same data).

Also, do not confuse temporary tables with intermediate tables, as these are different concepts.
However, when caching, the temporary table can become an intermediate table (association/intersection table).
