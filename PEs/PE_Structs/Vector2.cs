using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_Structs
{
    internal struct Vector2
    {
        // *** FIELDS ***
        private float valueX;
        private float valueY;

        // *** PROPERTIES ***
        /// <summary>
        /// Read & Write Property for valueX
        /// </summary>
        public float ValueX
        {
            get
            {
                return valueX;
            }
            set
            {
                valueX = value;
            }
        }

        /// <summary>
        /// Read & Write Property for valueY
        /// </summary>
        public float ValueY
        {
            get
            {
                return valueY;
            }
            set
            {
                valueY = value;
            }
        }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor to get the x- and y-values 
        /// </summary>
        /// <param name="valueX">Float value of X</param>
        /// <param name="valueY">Float value of Y</param>
        public Vector2(float valueX, float valueY)
        {
            this.valueX = valueX;
            this.valueY = valueY;
        }

        // *** METHODS ***
        /// <summary>
        /// Prints out the x- and y-values in proper formatting 
        /// </summary>
        /// <returns>X- and Y- Values</returns>
        public override string ToString()
        {
            return $"({valueX},{valueY})";
        }
    }
}
