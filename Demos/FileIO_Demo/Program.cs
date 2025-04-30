using System.IO;

namespace FileIO_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = null;

            List<string> name = new List<string>();
            List<int> age = new List<int>();
            List<bool> pickles = new List<bool>();

            // Errors could occur if the file doesn't exist, or if the path is incorrect.
            try
            {

                // Create a StreamReader object
                reader = new StreamReader("../../../MyTextFile.txt");
                string lineOfText = "";

                while ((lineOfText = reader.ReadLine()!) != null)
                {
                    string[] splitLineData = lineOfText.Split('|');
                    // 0 --> name
                    // 1 --> age 
                    // 2 --> pickles
                    name.Add(splitLineData[0]);
                    age.Add(int.Parse(splitLineData[1]));
                    pickles.Add(bool.Parse(splitLineData[2]));

                    // What do I do with lineOfText then ?
                    // Print it to the console... 
                    // save it in a variable or data structure... 
                    // write it back to another file...
                    // use it to instantiate an object...
                    // really, anything you choose!! :)

                    // Let's see if it was read by the program!
                    //Console.WriteLine("My program read: " + lineOfText);
                    //age.Add(int.Parse(lineOfText));
                }

            }
            // Must use a catch with a try!
            catch (Exception error)
            {
                //Console.WriteLine("Error occurred reading the file: " + error.Message);
            }
            finally
            {

                if (reader != null)
                {
                    // Close the stream, which closes the file
                    reader.Close();
                }
            }

            //-----------------------------------------------------------
            StreamWriter writer = new StreamWriter("../../../MyTextFile.txt");
            try
            {
                writer.WriteLine("Something else here.");
                //writer.WriteLine("And this is more data");
                writer.Write("and more here.");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error ocurred writing to the file:" + error.Message);
            }
            finally
            {
                if (writer != null)
                {
                    // Close the stream, which closes the file
                    writer.Close();
                }
            }
        }
    }
}
