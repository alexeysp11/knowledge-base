# ExpressionTreesDotnet

IEnumerable и IQueryable отличаются тем, что IEnumerable представляет коллекцию объектов, которую можно перебирать последовательно, а IQueryable представляет запрос к базе данных, который еще не был выполнен. Это означает, что при использовании IQueryable запрос к базе данных будет выполнен только тогда, когда будет вызван метод, который вернет результат.

Деревья выражений в EF Core и LINQ - это структуры данных, которые представляют логические выражения в виде дерева, где каждый узел представляет операцию или значение. 
Они используются для создания динамических запросов к базе данных и позволяют строить сложные запросы без необходимости написания SQL-кода вручную.

## Запросы

Ниже приведены 10 задач, которые помогут понять, как работать с деревьями выражений в EF Core и LINQ:

1. &cross; Создайте запрос, который выбирает все записи из таблицы Employees, где возраст сотрудника меньше 30 лет.
2. &cross; Напишите запрос, который выбирает все записи из таблицы `FinancialTransactions_Orders`, где сумма заказа больше 1000 долларов.
3. &cross; Создайте запрос, который выбирает все записи из таблицы `FinancialTransactions_Products`, где название продукта содержит слово "Apple".
4. &cross; Напишите запрос, который выбирает все записи из таблицы Customers, где имя клиента начинается с буквы "A".
5. &cross; Создайте запрос, который выбирает все записи из таблицы Employees, где зарплата сотрудника больше 50000 долларов в год.
6. &cross; Напишите запрос, который выбирает все записи из таблицы `FinancialTransactions_Orders`, где дата заказа находится в промежутке между 1 января 2021 года и 31 декабря 2021 года.
7. &cross; Создайте запрос, который выбирает все записи из таблицы Customers, где страна клиента равна "USA".
8. &cross; Напишите запрос, который выбирает все записи из таблицы `FinancialTransactions_Products`, где цена продукта больше 50 долларов.
9. &cross; Создайте запрос, который выбирает все записи из таблицы Employees, где должность сотрудника равна "Manager".
10. &cross; Напишите запрос, который выбирает все записи из таблицы `FinancialTransactions_Orders`, где количество заказанных единиц товара больше 10.

## Expression

1. &cross; Напишите метод, который принимает на вход выражение типа Expression<Func<T, bool>> и возвращает строку SQL-запроса, соответствующую этому выражению.
```C#
using System.Linq.Dynamic.Core;

public string GetSqlQuery<T>(Expression<Func<T, bool>> expression)
{
    var query = expression.ToSql();
    return query;
}
```

2. &cross; Создайте запрос, который выбирает все записи из таблицы `FinancialTransactions_Orders`, где сумма заказа больше средней суммы заказов.
3. &cross; Напишите метод, который принимает на вход выражение типа Expression<Func<T, bool>> и возвращает новое выражение, которое добавляет к нему условие "или".
```C#
public Expression<Func<T, bool>> AddOrCondition<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
{
    var parameter = Expression.Parameter(typeof(T), "x");
    var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
    var left = leftVisitor.Visit(expression1.Body);
    var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
    var right = rightVisitor.Visit(expression2.Body);
    var orElse = Expression.OrElse(left, right);
    return Expression.Lambda<Func<T, bool>>(orElse, parameter);
}

public class ReplaceExpressionVisitor : ExpressionVisitor
{
    private readonly Expression _oldValue;
    private readonly Expression _newValue;

    public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    {
        _oldValue = oldValue;
        _newValue = newValue;
    }

    public override Expression Visit(Expression node)
    {
        if (node == _oldValue)
        {
            return _newValue;
        }
        return base.Visit(node);
    }
}
```

Пример использования:
```C#
Expression<Func<int, bool>> expression1 = x => x > 5;
Expression<Func<int, bool>> expression2 = x => x < 3;
var expression3 = AddOrCondition(expression1, expression2);
Console.WriteLine(expression3.Compile()(2)); // true
Console.WriteLine(expression3.Compile()(4)); // true
Console.WriteLine(expression3.Compile()(6)); // true
Console.WriteLine(expression3.Compile()(8)); // false
```

4. &cross; Создайте запрос, который выбирает все записи из таблицы `FinancialTransactions_Products`, где цена продукта меньше 10 долларов или больше 100 долларов.
5. &cross; Напишите метод, который принимает на вход выражение типа Expression<Func<T, bool>> и возвращает новое выражение, которое добавляет к нему условие "не равно".
6. &cross; Создайте запрос, который выбирает все записи из таблицы Customers, где имя клиента содержит более одного слова.
7. &cross; Напишите метод, который принимает на вход выражение типа Expression<Func<T, bool>> и возвращает новое выражение, которое добавляет к нему условие "больше или равно".
8. &cross; Создайте запрос, который выбирает все записи из таблицы Employees, где дата начала работы сотрудника находится в промежутке между 1 января 2010 года и 31 декабря 2020 года.
9. &cross; Напишите метод, который принимает на вход выражение типа Expression<Func<T, bool>> и возвращает новое выражение, которое добавляет к нему условие "меньше".
10. &cross; Создайте запрос, который выбирает все записи из таблицы `FinancialTransactions_Orders`, где количество заказанных единиц товара больше среднего количества заказанных единиц товара.

## Деревья выражений как структура данных 

Задачи для улучшения навыков работы с деревьями выражений в C#.

1. &cross; Напишите метод, который принимает строку математического выражения в виде инфиксной записи и возвращает дерево выражения в виде бинарного дерева.
2. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и возвращает его в виде постфиксной записи.
3. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и вычисляет его значение.
4. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и проверяет, является ли оно сбалансированным.
5. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и проверяет, является ли оно симметричным.
6. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и выводит его на экран в виде графа.
7. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и преобразует его в дерево разбора (parse tree).
8. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и преобразует его в дерево выражений (expression tree).
9. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и преобразует его в обратную польскую запись.
10. &cross; Напишите метод, который принимает дерево выражения в виде бинарного дерева и оптимизирует его, удаляя лишние узлы и упрощая выражение.
