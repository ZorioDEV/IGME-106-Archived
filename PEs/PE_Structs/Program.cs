namespace PE_Structs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            Vector2 myCenter = new Vector2(6, 19);
            Circle myCircle1 = new Circle(5.3f, 5, 3.7f);
            Circle myCircle2 = new Circle(1, new Vector2(20, 10));

            // *** MAIN CODE ***
            // prints the center's and both cirlces' information
            Console.WriteLine("--- Initial data ---");
            Console.WriteLine(myCenter);
            Console.WriteLine(myCircle1);
            Console.WriteLine(myCircle2);
            Console.WriteLine();

            // changes the center's y-value to 27
            myCenter.ValueY = 27;

            // changes the center of myCircle2 so the x-value is increased by 5
            myCircle2.Center = new Vector2(myCircle2.Center.ValueX + 5, myCircle2.Center.ValueY);

            // changes the radius of myCircle2
            myCircle2.Radius = 2.5f;

            // prints the changed center's and both cirlces' information
            Console.WriteLine("--- Changed data ---");
            Console.WriteLine(myCenter);
            Console.WriteLine(myCircle1);
            Console.WriteLine(myCircle2);
            Console.WriteLine();

            // creates a list of the already created Circles
            List<Circle> myCircles = new List<Circle>();
            myCircles.Add(myCircle1);
            myCircles.Add(myCircle2);

            // prints the list of cirlces' information
            Console.WriteLine("--- Data from circles referenced via the list. No changes. ---");
            for (int i = 0; i < myCircles.Count; i++)
            {
                Console.WriteLine(myCircles[i]);
            }
            Console.WriteLine();

            // changes the center of myCircle2 so the y-value is increased by 2
            Circle tempMyCircle2 = myCircles[1];
            tempMyCircle2.Center = new Vector2(tempMyCircle2.Center.ValueX, tempMyCircle2.Center.ValueY + 2);
            myCircles[1] = tempMyCircle2;

            // changes the radius of myCircle1
            Circle tempMyCircle1 = myCircles[0];
            tempMyCircle1.Radius = 37;
            myCircles[0] = tempMyCircle1;

            // prints the changed list of cirlces' information
            Console.WriteLine("--- Data from circles referenced via the list AFTER move and resize ---");
            for (int i = 0; i < myCircles.Count; i++)
            {
                Console.WriteLine(myCircles[i]);
            }
        }
    }
}
