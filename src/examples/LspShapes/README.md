# LspShapes

[English](README.md) | [Русский](README.ru.md)

## Library 

C# library for calculating area of a circle and triangle. 

Additional requirements: 

- Unit tests; 
- It should be easy to add another shapes; 
- Calculating area of a shape dynamically, without knowing its type; 
- Checking if a triangle is right-angled. 

### Solution 

1. Create an object and invoke its methods

Classes `Triangle` and `Circle` implement `IShape` interface, so you can use them in the following way: 

```C#
IShape triangle = new Triangle(3, 4, 5); 
IShape circle = new Circle(4.5); 

double triangleArea = triangle.GetArea(); 
double circleArea = circle.GetArea(); 
```

2. Redefining parameters of a shape 

If you need to redefine some of the parameters of a shape, it's necessary to create a new object, which implements `IShape` interface. 
Explicit redefining parameters of a shape (e.g. lengths of a triangle's sides, or circle radius) is not allowed if you suppose to use `IShape` interface, because the **Liskov Substitution Principle** is going to be violated, and `NotImplementedException` could accedentally occur on a client side. 

3. Getting specific info about a shape 

Because of the potential risk of violation of the **Liskov Substitution Principle**, it's better to check, if a triangle is right-angled, using `ShapeInfo` class, which is going to be common for all the shapes (so specific info about the shape is stored in the `SpecificInfo` variable):

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

Hence, in order to define if a triangle is right-angled, `GetInfo()` method is used. 
This method could also be used to get an area of a shape: 

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

`SpecificInfo` is also could be easily extended for getting a specific info about other shapes (e.g. calculating a chord of a circle). 

Note: in the example above, `ShapeType` field is not used, because a type of the shape was specified expicitly. 
However if you get the shape object as a parameter, and you don't know its type exactly, the using of `ShapeType` field is required:

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

## Retrieving data from DB 

There're tables `product` and `category` in a MS SQL database. 
Multiple records in the `product` table are associated with multiple records in the `category` table. 

Write SQL request for getting records in a form "Product name - Category name". 
If a product does not have any category, its name also should be displayed. 

### Initialization 

File for initialing the database and the table inside it, is located in `sql` folder (file `initdb.sql`). 

### SQL request 

SQL request is located in `sql` folder (file `select.sql`). 

```SQL
SELECT 
    p.name AS product_name, 
    c.name AS category_name
FROM [LspShapesDb].[dbo].[product] p 
LEFT JOIN [LspShapesDb].[dbo].[product_category] pc ON pc.product_id = p.product_id 
LEFT JOIN [LspShapesDb].[dbo].[category] c ON c.category_id = pc.category_id
```
