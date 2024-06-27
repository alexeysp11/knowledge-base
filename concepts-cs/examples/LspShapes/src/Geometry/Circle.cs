using WorkflowLib.Examples.LspShapes.Models; 

namespace WorkflowLib.Examples.LspShapes.Geometry
{
    public class Circle : IShape
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private double Radius { get; set; }
        #endregion  // Properties

        #region Constructors
        /// <summary>
        /// Default constructor 
        /// </summary>
        public Circle() : this(0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Circle(double radius)
        {
            if (radius < 0) throw new System.Exception("Radius of a circle could not be negative"); 

            Radius = radius; 
        }
        #endregion  // Constructors

        #region Public methods 
        /// <summary>
        /// Calculates an area of the circle 
        /// </summary>
        public double GetArea()
        {
            return System.Math.PI * System.Math.Pow(Radius, 2); 
        }

        /// <summary>
        /// Returns all the information about the circle 
        /// </summary>
        public ShapeInfo GetInfo()
        {
            return new ShapeInfo()
            {
                ShapeType = ShapeType.Circle, 
                SpecificInfo = new CircleInfo() 
                {
                    Radius = this.Radius, 
                    IsPointCircle = this.IsPointCircle()
                },
                Area = this.GetArea()
            }; 
        }
        #endregion  // Public methods 

        #region Private methods
        /// <summary>
        /// Checks if the radius of a circle is zero
        /// </summary>
        private bool IsPointCircle()
        {
            return Radius == 0; 
        }
        #endregion  // Private methods
    }
}