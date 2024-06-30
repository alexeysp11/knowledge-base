namespace WorkflowLib.Examples.LspShapes.Models
{
    public class CircleInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// If the radius of a circle be zero, then the circle is called a point circle.
        /// </summary>
        public bool IsPointCircle { get; set; }
    }
}