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