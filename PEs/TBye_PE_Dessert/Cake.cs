using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TBye_PE_Dessert
{
    internal class Cake : Dessert
    {
        // *** FEILDS ***
        /// <summary>
        /// All possible flavors of cake and frosting.
        /// </summary>
        public enum Flavor
        {
            Vanilla,
            Chocolate,
            Strawberry,
            Lemon,
            Coconut
        }

        private Flavor cakeFlavor;
        private Flavor frostingFlavor;
        private int numberOfLayers;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-Only properties of the cake's flavor.
        /// </summary>
        public Flavor CakeFlavor
        {
            get
            {
                return cakeFlavor;
            }
        }

        /// <summary>
        /// Read-Only properties of the frosting's flavor.
        /// </summary>
        public Flavor FrostingFlavor
        {
            get
            {
                return frostingFlavor;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Child constructor for the Cake object.
        /// </summary>
        /// <param name="cakeFlavor">Flavor of the Cake</param>
        /// <param name="frostingFlavor">Flavor of the frosting</param>
        /// <param name="numberOfLayers">Number of Layors the Cake has</param>
        public Cake(Flavor cakeFlavor, Flavor frostingFlavor, int numberOfLayers) :
                    base (10.00)
        {
            this.cakeFlavor = cakeFlavor;
            this.frostingFlavor = frostingFlavor;
            this.numberOfLayers = numberOfLayers;
        }

        // *** METHODS ***
        /// <summary>
        /// Prints out the Cake's information.
        /// </summary>
        public override void PrintInformation()
        {
            Console.WriteLine($"--- Cake Order ---" +
                $"\n{numberOfLayers} layer {cakeFlavor.ToString().ToLower()} cake with {frostingFlavor.ToString().ToLower()} frosting." +
                $"\nTotal: {CalculatePrice():C2}\n");
        }

        /// <summary>
        /// Calculates the price of the Cake's depending on their flavor & frosting.
        /// </summary>
        /// <returns>Final price of the Cake</returns>
        public override double CalculatePrice()
        {
            // raises the price by $1 if the layers are greater than one
            if (numberOfLayers > 1)
            {
                Price += (double)(numberOfLayers - 1);
            }

            // raise the price by $5 if special cakeFlavor
            if (cakeFlavor == Flavor.Coconut || cakeFlavor == Flavor.Lemon)
            {
                Price += 5;
            }

            // raise the price by $5 if special frostingFlavor
            if (frostingFlavor == Flavor.Coconut || frostingFlavor == Flavor.Lemon)
            {
                Price += 5;
            }

            // decreases the price by 10% if Cake's flavor and frosting are the same
            if (cakeFlavor == frostingFlavor)
            {
                Price -= Price * 0.1;
            }

            return Price;
        }
    }
}
