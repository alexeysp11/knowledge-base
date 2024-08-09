using WorkflowLib.Examples.LspShapes.Models;

namespace WorkflowLib.Examples.LspShapes.Geometry
{
    public interface IShape
    {
        double GetArea(); 
        ShapeInfo GetInfo(); 
    }
}