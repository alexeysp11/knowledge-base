# LspShapes

[English](README.md) | [Русский](README.ru.md)

## Библиотека 

Библиотека на C# для поставки внешним клиента, которая умеет вычислять площадь круга по радиусу и треугольника по трем сторонам. 

Дополнительные требования: 

- Юнит-тесты; 
- Легкость добавления других фигур; 
- Вычисление площади фигуры без знания типа фигуры в compile-time; 
- Проверка на то, является ли треугольник прямоугольным. 

### Поянения к коду 

1. Создание объекта и вызов методов 

Классы `Triangle` и `Circle` реализуют общий интерфейс `IShape`, соответственно, они создаются и используются следующим образом: 

```C#
IShape triangle = new Triangle(3, 4, 5); 
IShape circle = new Circle(4.5); 

double triangleArea = triangle.GetArea(); 
double circleArea = circle.GetArea(); 
```

2. Переопределение параметров фигуры 

Если необходимо переопределить параметры фигуры, то нужно создать новый объект, реализующий интерфейс `IShape` аналогично примеру, приведенному выше. 
Переопределение параметров геометрической фигуры (например, длины стороны треугольника или радиуса круга) с помощью публичных методов не предусмотрено интерфейсом `IShape`, поскольку в противном случае будет нарушен **Liskov Substitution Principle**, т.е. в клиентском коде может неожиданно возникнуть `NotImplementedException`. 

3. Получение специфичной информации о фигуре 

Также по причине возможного нарушения **Liskov Substitution Principle** получение информации о том, является ли треугольник прямоугольным, лучше выполнить с помощью объекта класса `ShapeInfo`, который будет общим для всех типов фигур (специфичная информация о геометрической фигуре записывается в переменную `SpecificInfo`): 

```C#
namespace WorkflowLib.Examples.LspShapes.Models
{
    /// <summary>
    /// Class for stroring information about the shape 
    /// </summary>
    public class ShapeInfo
    {
        /// <summary>
        /// Allows to understand how to unbox SpecificInfo
        /// </summary>
        public ShapeType ShapeType { get; set; }

        /// <summary>
        /// Information which is spefic for each of the shape 
        /// </summary>
        public object SpecificInfo { get; set; }
        
        /// <summary>
        /// Calculated area 
        /// </summary>
        public double Area { get; set; }
    }
}
```

Соответственно, чтобы определить является ли треугольник прямоугольным, используется метод `GetInfo()`. 
Кроме того, площадь фигуры тоже можно найти, используя этот метод:

```C#
// Initialize the object 
IShape triangle = new Triangle(); 

// Retrieve the triangle area
double triangleArea = triangle.GetArea();

// Get info about the shape 
ShapeInfo info = triangle.GetInfo(); 
TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 

// Store retrieved data in the variables
bool isTriangleRight = triangleInfo.IsRightTriangle; 
double infoArea = info.Area; 
```

Аналогичным образом, с помощью `SpecificInfo` можно обеспечить получение специфичной информации для других фигур (например, расчет хорды для круга). 

Замечание: в приведенном выше примере не используется поле `ShapeType`, поскольку был явно указан тип фигуры. 
Но если в клиентском коде не известен тип фигуры (или не задан явно), то использование поля `ShapeType` является необходимым, например: 

```C#
public void ProcessShapeInfo(IShape shape)
{
    ShapeInfo info = shape.GetInfo();
    switch (info.ShapeType)
    {
        case ShapeType.Triangle: 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            System.Console.WriteLine($"Type: {info.ShapeType.ToString()}, A: {triangleInfo.A}, B: {triangleInfo.B}, C: {triangleInfo.C}"); 
            break; 
        case ShapeType.Circle: 
            CircleInfo circleInfo = (CircleInfo)info.SpecificInfo; 
            System.Console.WriteLine($"Type: {info.ShapeType.ToString()}, Radius: {circleInfo.Radius}"); 
            break; 
        default: 
            System.Console.WriteLine("Default behavior"); 
            break; 
    }
}
```

## Выгрузка данных из БД 

В базе данных MS SQL есть продукты и категории. 
Одному продукту может соответствовать много категорий, в одной категории может быть много продуктов. 

Написать SQL-запрос для выбора пар "Имя продукта - Имя категории". 
Если у продукта нет категории, то его имя должно всё равно выводиться.  

### Инициализация базы данных и таблиц 

Файл, который содержит скрипт для инициализации базы данных и таблиц, находится в папке `sql` (файл `initdb.sql`).  

### SQL-запрос

SQL-запрос находится в папке `sql` (файл `select.sql`): 

```SQL
SELECT 
    p.name AS product_name, 
    c.name AS category_name
FROM [LspShapesDb].[dbo].[product] p 
LEFT JOIN [LspShapesDb].[dbo].[product_category] pc ON pc.product_id = p.product_id 
LEFT JOIN [LspShapesDb].[dbo].[category] c ON c.category_id = pc.category_id
```
