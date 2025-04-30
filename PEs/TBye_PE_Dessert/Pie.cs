using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBye_PE_Dessert
{
    internal class Pie : Dessert
    {
        // *** FEILDS ***
        /// <summary>
        /// All possible filling flavors for Pie.
        /// </summary>
        public enum Filling
        {
            Apple,
            Pumpkin,
            Strawberry,
            ChocolateCream
        }

        private Filling typeOfPie;
        private string chocolateCreamString = "Chocolate cream";

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Child constructor for the Pie object.
        /// </summary>
        /// <param name="typeOfPie">Flavor of the Pie</param>
        public Pie(Filling typeOfPie) :
                    base(8.00)
        {
            this.typeOfPie = typeOfPie;
        }

        // *** METHODS ***
        /// <summary>
        /// Prints out the Pie's information.
        /// </summary>
        public override void PrintInformation()
        {
            // tests if the filling is ChocholateCream & formats properly
            if (typeOfPie == Filling.ChocolateCream)
            {
                Console.WriteLine($"--- Pie Order ---" +
                    $"\n{chocolateCreamString} pie" +
                    $"\nTotal: {CalculatePrice():C2}\n");
            }
            else
            {
                Console.WriteLine($"--- Pie Order ---" +
                    $"\n{typeOfPie.ToString()} pie" +
                    $"\nTotal: {CalculatePrice():C2}\n");
            }
        }

        /// <summary>
        /// Calculates the price of the Pie's depending on their fillings.
        /// </summary>
        /// <returns>Final price of the Pie</returns>
        public override double CalculatePrice()
        {
            // raises the price if the name of the flavor is more than 10 characters long
            if (typeOfPie.ToString().Length >= 10)
            {
                // tests if the filling is ChocholateCream & formats properly
                if (typeOfPie == Filling.ChocolateCream)
                {
                    Price = (double)chocolateCreamString.Length;
                }
                else
                {
                    Price = (double)typeOfPie.ToString().Length;
                }
            }

            return Price;
        }
    }
}
