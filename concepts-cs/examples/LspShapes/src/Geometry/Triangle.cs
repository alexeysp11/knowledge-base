using WorkflowLib.Examples.LspShapes.Models; 

namespace WorkflowLib.Examples.LspShapes.Geometry
{
    public class Triangle : IShape
    {
        #region Properties 
        /// <summary>
        /// 
        /// </summary>
        private double A { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private double B { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private double C { get; set; }
        #endregion  // Properties 

        #region Constructors
        /// <summary>
        /// Default constructor 
        /// </summary>
        public Triangle() : this(0, 0, 0)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Triangle(double a, double b, double c)
        {
            if (a < 0 || b < 0 || c < 0) throw new System.Exception("None of the specified sides of a triangle could be negative"); 

            A = a; 
            B = b; 
            C = c; 
        }
        #endregion  // Constructors

        #region Public methods
        /// <summary>
        /// Calculates an area of the triangle using the Heron's formula 
        /// </summary>
        public double GetArea()
        {
            double s = (A + B + C) / 2;     //Semi-perimeter 
            return System.Math.Sqrt(s*(s-A)*(s-B)*(s-C)); 
        }

        /// <summary>
        /// Returns all the information about the triangle
        /// </summary>
        public ShapeInfo GetInfo()
        {
            return new ShapeInfo() 
            {
                ShapeType = ShapeType.Triangle, 
                SpecificInfo = new TriangleInfo() 
                {
                    A = this.A, 
                    B = this.B, 
                    C = this.C, 
                    IsRightTriangle = this.IsRightTriangle(), 
                    IsDegenerateTriangle = this.IsDegenerateTriangle()
                },
                Area = this.GetArea()
            }; 
        }
        #endregion  // Public methods

        #region Private methods 
        /// <summary>
        /// Gets if the triangle is right using the Pythagorean theorem
        /// </summary>
        private bool IsRightTriangle()
        {
            double a2 = System.Math.Pow(A, 2);
            double b2 = System.Math.Pow(B, 2);
            double c2 = System.Math.Pow(C, 2);

            return a2 == b2 + c2 || b2 == a2 + c2 || c2 == a2 + b2; 
        }
        /// <summary>
        /// Checks if one of the triangle's sides is equal to zero
        /// </summary>
        private bool IsDegenerateTriangle()
        {
            return A == 0 || B == 0 || C == 0; 
        }
        #endregion  // Private methods 
    }
}