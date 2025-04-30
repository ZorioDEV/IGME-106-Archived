using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBye_PE_Dessert
{
    internal abstract class Dessert
    {
        // *** FEILDS ***
        private double price;

        // *** PROPERTIES ***
        /// <summary>
        /// Read & Write property of the Dessert's price.
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Parent constructor for the Dessert object.
        /// </summary>
        /// <param name="price">Price of the Dessert</param>
        public Dessert(double price)
        {
            this.price = price;
        }

        // *** METHODS ***
        /// <summary>
        /// Prints out the Desserts's information.
        /// </summary>
        public abstract void PrintInformation();

        /// <summary>
        /// Returns the price of the given dessert.
        /// </summary>
        public abstract double CalculatePrice();
    }
}
