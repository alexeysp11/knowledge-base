using System;
using Xunit;
using WorkflowLib.Examples.LspShapes.Geometry; 
using WorkflowLib.Examples.LspShapes.Models; 

namespace WorkflowLib.Examples.LspShapes.Tests.Geometry
{
    public class TriangleTest
    {
        [Fact]
        public void Triangle_DefaultConstructor_DegenerateTriangleAndZeroArea()
        {
            // Arrange
            IShape triangle = new Triangle(); 

            // Act 
            ShapeInfo info = triangle.GetInfo(); 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            double area = triangle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Triangle); 
            Assert.True(triangleInfo.IsDegenerateTriangle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(0, area, 4);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        public void Triangle_ZeroParameters_DegenerateTriangleAndZeroArea(double a, double b, double c)
        {
            // Arrange
            IShape triangle = new Triangle(a, b, c); 

            // Act 
            ShapeInfo info = triangle.GetInfo(); 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            double area = triangle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Triangle); 
            Assert.True(triangleInfo.IsDegenerateTriangle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(0, area, 4);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 0, 0)]
        public void Triangle_ZeroParameters_DegenerateTriangleAndNaNArea(double a, double b, double c)
        {
            // Arrange
            IShape triangle = new Triangle(a, b, c); 

            // Act 
            ShapeInfo info = triangle.GetInfo(); 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            double area = triangle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Triangle); 
            Assert.True(triangleInfo.IsDegenerateTriangle); 
            Assert.True(double.IsNaN(area)); 
            Assert.True(double.IsNaN(info.Area)); 
        }

        [Theory]
        [InlineData(-1, -1, -1)]
        [InlineData(-1, -1, 1)]
        [InlineData(-1, 1, -1)]
        [InlineData(-1, 1, 1)]
        [InlineData(1, 1, -1)]
        [InlineData(-15, -15, -15)]
        [InlineData(-15, -15, 1)]
        [InlineData(-15, 1, -15)]
        [InlineData(-15, 1, 1)]
        [InlineData(1, -15, -15)]
        [InlineData(1, -15, 1)]
        [InlineData(1, 1, -15)]
        [InlineData(-515, -515, -515)]
        [InlineData(-515, -515, 1)]
        [InlineData(-515, 1, -515)]
        [InlineData(-515, 1, 1)]
        [InlineData(1, -515, -515)]
        [InlineData(1, -515, 1)]
        [InlineData(1, 1, -515)]
        public void Triangle_NegativeParameter_ReturnsException(double a, double b, double c)
        {
            // Arrange/Act 
            Action act = () => { IShape triangle = new Triangle(a, b, c); };

            // Assert 
            System.Exception exception = Assert.Throws<System.Exception>(act);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 7, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(1, 3, 1)]
        [InlineData(1, 1, 4)]
        [InlineData(1, 6, 1)]
        [InlineData(1, 5, 10)]
        [InlineData(15, 15, 15)]
        [InlineData(15, 25, 1)]
        [InlineData(15, 1, 15)]
        [InlineData(15, 1, 1)]
        [InlineData(1, 15, 15)]
        [InlineData(1, 15, 1)]
        [InlineData(1, 1, 15)]
        [InlineData(515, 515, 515)]
        [InlineData(515, 515, 1)]
        [InlineData(515, 1, 245)]
        [InlineData(515, 1, 1)]
        [InlineData(1, 585, 515)]
        [InlineData(1, 515, 1)]
        [InlineData(1, 1, 515)]
        public void Triangle_CorrectParameters_CorrectAreaAndNotADegenerateTriangle(double a, double b, double c)
        {
            // Arrange
            IShape triangle = new Triangle(a, b, c); 

            // Act 
            ShapeInfo info = triangle.GetInfo(); 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            double area = triangle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Triangle); 
            Assert.False(triangleInfo.IsDegenerateTriangle); 
            Assert.False(triangleInfo.IsRightTriangle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(GetExpectedArea(a, b, c), info.Area, 4);
        }

        [Theory]
        [InlineData(5, 4, 3)]
        [InlineData(5, 3, 4)]
        [InlineData(4, 3, 5)]
        [InlineData(3, 4, 5)]
        public void Triangle_RightTriangle_True(double a, double b, double c)
        {
            // Arrange
            IShape triangle = new Triangle(a, b, c); 

            // Act 
            ShapeInfo info = triangle.GetInfo(); 
            TriangleInfo triangleInfo = (TriangleInfo)info.SpecificInfo; 
            double area = triangle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Triangle); 
            Assert.False(triangleInfo.IsDegenerateTriangle); 
            Assert.True(triangleInfo.IsRightTriangle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(GetExpectedArea(a, b, c), info.Area, 4);
        }

        private double GetExpectedArea(double a, double b, double c)
        {
            double s = (a + b + c) / 2;     //Semi-perimeter 
            return System.Math.Sqrt(s*(s-a)*(s-b)*(s-c)); 
        }
    }
}
