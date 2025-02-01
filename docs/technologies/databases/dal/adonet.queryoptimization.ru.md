# Оптимизация скорости выполнения запросов в контексте использования ADO.NET

## Описание проблемы

Допустим, есть участок кода следующего вида:
```C#
public static DataTable ExecuteDataTableSqlDA(SqlConnection connection, string cmdText)
{
    var dt = new DataTable();
    var da = new SqlDataAdapter(cmdText, connection);
    da.Fill(dt);
    return dt;
}
```

В данном случае, используется класс `SqlDataAdapter` и, в частности, метод `SqlDataAdapter.Fill` для получения данных из запроса и заполнения таблицы `dt`.

Например, мы используем некоторый клиент для БД ([pgAdmin](https://github.com/pgadmin-org/pgadmin4), [DataGrip](https://www.jetbrains.com/datagrip/), [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) etc), и обнаружили, что запрос `cmdText` выполняется, допустим, `n` секунд. Однако когда мы передаем тот же самый запрос `cmdText` в метод `ExecuteDataTableSqlDA`, то выполнение метода `SqlDataAdapter.Fill` из кода на C# занимает в 2-3 раза больше времени.

### Возможные причины

**Неэффективная работа с типами данных**: при заполнении переменной `dt` создается довольно много объектов (в том числе `DataRow`, `DataColumn`). Классы `DataRow` и `DataColumn` приводят каждое значение ячейки данных к типу `object`, в результате этого, будет производиться boxing/unboxing каждый раз при работе с данными (см. [DataRow.Item[]](https://learn.microsoft.com/en-us/dotnet/api/system.data.datarow.item) и [DataRow.ItemArray](https://learn.microsoft.com/en-us/dotnet/api/system.data.datarow.itemarray)).

### Возможные решения

### Dapper

ORM (Object-Relational Mapper) — это  более эффективная альтернатива SqlDataAdapter для многих сценариев. Если мы попробуем минимизировать неэффективную работу с типами данных, то возможным решением может быть Dapper, который позволяет явно конвертировать результаты запроса к нужным типам данных и избежать лишнего boxing/unboxing.

Аналогичный код с использованием Dapper будет выглядеть примерно следующим образом:
```C#
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

public static List<YourObjectType> ExecuteQueryDapper(SqlConnection connection, string cmdText)
{
    return connection.Query<YourObjectType>(cmdText).ToList();
}

public class YourObjectType
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

## Исследование времени выполнения запросов

### BenchmarkDotNet

Есть инструмент [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet), который позволяет измерять производительность кода. Данный инструмент отлично подходит для измерения производительности кода, который обрабатывает данные (например, преобразование данных, обработка списков, работа с коллекциями). Но он не предназначен для измерения производительности самого запроса к БД и времени его выполнения на сервере БД.

Причина заключается в том, что BenchmarkDotNet выполняет большое количество вызовов одного и того же участка кода, в результате чего получает большую выборку данных, в которых гораздо меньше статистических выбросов. Это позволяет рассчитать различные величины более точно (математическое ожидание, стандартное отклонение и т.д.).

Такой подход работает, когда вызываемый участок кода каждый раз будет затрачивать примерно равные промежутки времени. Однако при тестировании скорости выполнения запроса к БД этот подход даст некорректные результаты из-за того, что большинство СУБД кэшируют планы выполнения запросов и иногда сами результаты запросов. В результате этого, долго выполнится только первый запрос, а все остальные будут использовать уже готовый план и выполнятся гораздо быстрее.

### Использование разных таблиц

Можно создать три таблицы с одинаковой структурой и наполнением (данные можно заполнить скриптом):
1. Таблица для тестирования Dapper;
2. Таблица для тестирования `SqlDataAdapter`;
3. Таблица для тестирования через командную строку/pgAdmin.
