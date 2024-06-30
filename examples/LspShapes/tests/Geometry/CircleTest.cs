using System;
using Xunit;
using WorkflowLib.Examples.LspShapes.Geometry; 
using WorkflowLib.Examples.LspShapes.Models; 

namespace WorkflowLib.Examples.LspShapes.Tests.Geometry
{
    public class CircleTest
    {
        [Fact]
        public void Circle_DefaultConstructor_PointCircleAndZeroArea()
        {
            // Arrange
            IShape circle = new Circle(); 

            // Act 
            ShapeInfo info = circle.GetInfo(); 
            CircleInfo circleInfo = (CircleInfo)info.SpecificInfo; 
            double area = circle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Circle); 
            Assert.True(circleInfo.IsPointCircle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(0, area, 4);
        }

        [Fact]
        public void Circle_ZeroRadius_PointCircleAndZeroArea()
        {
            // Arrange
            IShape circle = new Circle(0); 

            // Act 
            ShapeInfo info = circle.GetInfo(); 
            CircleInfo circleInfo = (CircleInfo)info.SpecificInfo; 
            double area = circle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Circle); 
            Assert.True(circleInfo.IsPointCircle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(0, area, 4);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-55)]
        [InlineData(-100)]
        [InlineData(-5000)]
        [InlineData(-0.1)]
        public void Circle_NegativeParameter_ReturnsException(double radius)
        {
            // Arrange/Act 
            Action act = () => { IShape circle = new Circle(radius); };

            // Assert 
            System.Exception exception = Assert.Throws<System.Exception>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(55)]
        [InlineData(100)]
        [InlineData(5000)]
        [InlineData(0.1)]
        [InlineData(0.5)]
        public void Circle_CorrectParameter_CorrectAreaAndNotACirclePoint(double radius)
        {
            // Arrange
            IShape circle = new Circle(radius); 

            // Act 
            ShapeInfo info = circle.GetInfo(); 
            CircleInfo circleInfo = (CircleInfo)info.SpecificInfo; 
            double area = circle.GetArea(); 

            // Assert
            Assert.True(info.ShapeType == ShapeType.Circle); 
            Assert.False(circleInfo.IsPointCircle); 
            Assert.Equal(area, info.Area, 4);
            Assert.Equal(GetExpectedArea(radius), info.Area, 4);
        }

        private double GetExpectedArea(double radius)
        {
            return System.Math.PI * System.Math.Pow(radius, 2);
        }
    }
}
