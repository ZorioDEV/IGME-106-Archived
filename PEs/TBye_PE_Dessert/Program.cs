namespace TBye_PE_Dessert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            List<Dessert> myDesserts = new List<Dessert>
            {new Cake(Cake.Flavor.Vanilla, Cake.Flavor.Strawberry, 5),
            new Cake(Cake.Flavor.Chocolate, Cake.Flavor.Chocolate, 2),
            new Cake(Cake.Flavor.Lemon, Cake.Flavor.Vanilla, 3),
            new Cake(Cake.Flavor.Coconut, Cake.Flavor.Coconut, 3),
            new Pie(Pie.Filling.Apple),
            new Pie(Pie.Filling.Strawberry),
            new Pie(Pie.Filling.ChocolateCream)
            };

            double totalPrice = 0;
            double highestPrice = 0;
            Cake mostExpensiveCake = null;

            // *** MAIN CODE ***
            // loops through all Dessert objects & prints out their info
            foreach (Dessert dessert in myDesserts)
            {
                dessert.PrintInformation();

                // adds current dessert price to a total
                totalPrice += dessert.Price;
            }

            // prints out total price of all Dessert objects
            Console.WriteLine($"The total price of all items is {totalPrice:C2}\n");


            // loops through all Dessert objects
            foreach (Dessert dessert in myDesserts)
            {
                // filters out all objects that are not Cake objects

                if (dessert.GetType() == typeof(Cake))
                {
                    // finds the Cake object with the highest price
                    if (dessert.Price > highestPrice)
                    {
                        highestPrice = dessert.Price;
                        mostExpensiveCake = (Cake)dessert;
                    }
                }
            }

            // prints out the info of the most expensive cake
            Console.WriteLine($"The most expensive cake is the {mostExpensiveCake.CakeFlavor.ToString().ToUpper()}" +
                $" cake with {mostExpensiveCake.FrostingFlavor.ToString().ToUpper()} frosting.");
        }
    }
}
