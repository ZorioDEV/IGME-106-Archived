using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_Structs
{
    internal struct Circle
    {
        // *** FIELDS ***
        private float radius;
        private Vector2 center = new Vector2();

        // *** PROPERTIES ***
        /// <summary>
        /// Read & Write Property for radius
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        /// <summary>
        /// Read & Write Property for center
        /// </summary>
        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
            }
        }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// 1st constructor that takes radius and center point of the circle
        /// </summary>
        /// <param name="radius">Radius of the Circle</param>
        /// <param name="center">Center point of the Circle</param>
        public Circle(float radius, Vector2 center)
        {
            this.radius = radius;
            this.center = center;
        }

        /// <summary>
        /// 2nd constructor that takes radius, x-value, and y-value of the circle
        /// </summary>
        /// <param name="radius">Radius of the Circle</param>
        /// <param name="valueX">X-value of the center</param>
        /// <param name="valueY">Y-value of the center</param>
        public Circle(float radius, float valueX, float valueY)
        {
            this.radius = radius;
            this.center.ValueX = valueX;
            this.center.ValueY = valueY;
        }

        // *** METHODS ***
        /// <summary>
        /// Calculates the area of the given Circle
        /// </summary>
        /// <returns>Double of the area</returns>
        public double CalculateArea()
        {
            double area = Math.PI * Math.Pow((double)radius, 2);

            return area;
        }

        /// <summary>
        /// Prints out the Circle's data
        /// </summary>
        /// <returns>String of the circle's info</returns>
        public override string ToString()
        {
            return $"Circle center {center} with radius {radius}. Area is {CalculateArea()} units.";
        }
    }
}
