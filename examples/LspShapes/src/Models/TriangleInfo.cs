namespace WorkflowLib.Examples.LspShapes.Models
{
    public class TriangleInfo
    {
        /// <summary>
        ///
        /// </summary>
        public double A { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double B { get; set; }
        
        /// <summary>
        ///
        /// </summary>
        public double C { get; set; }

        /// <summary>
        /// A right triangle is a triangle that has one right (90°) angle.
        /// </summary>
        public bool IsRightTriangle { get; set; }

        /// <summary>
        /// If the distance between two points a and b is zero, then the corresponding vertex ¯ab is the zero vector, 
        /// which is collinear to every vector (which includes ¯ac and ¯bc). 
        /// Hence the vectors defining the triangle are all collinear, i.e., a degenerate triangle.
        /// </summary>
        public bool IsDegenerateTriangle { get; set; }
    }
}