using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnemyTextFileIO_STARTER
{
    /// <summary>
    /// Enemy object that stores Name's and Damages' of enemies
    /// </summary>
    public class Enemy
    {
        // *** PROPERTIES ***
        /// <summary>
        /// Read and Write Property for Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Read and Write Property for Damage
        /// </summary>
        public int Damage { get; set; }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor for Enemy
        /// </summary>
        /// <param name="name">Name of the Enemy</param>
        /// <param name="damage">Damage amount of the Enemy</param>
        public Enemy(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        // *** METHODS ***
        /// <summary>
        /// Override ToString to print out both name and damage of the Enemy
        /// </summary>
        /// <returns>Info of the Enemy</returns>
        public override string ToString()
        {
            return $"Name: {Name}. Damage: {Damage}";
        }

        /// <summary>
        /// Saves the Enemy's data to the text file
        /// </summary>
        /// <param name="filename">Pathing to the text file</param>
        /// <param name="enemies">List of Enemy objects</param>
        public static void SaveToFile(string filename, List<Enemy> enemies)
        {
            StreamWriter writer = null;

            // try-block for writer
            try
            {
                writer = new StreamWriter(filename);

                // adds name & damage amount for each character
                foreach (Enemy enemy in enemies)
                {
                    writer.WriteLine($"{enemy.Name},{enemy.Damage}");
                }
            }
            // runs if there is an error
            catch (Exception error)
            {
                Console.WriteLine("Error occurred reading the file: " + error.Message);
            }
            // Close the stream, which closes the file
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Loads the Enemy's data from the text file
        /// </summary>
        /// <param name="filename">Pathing to the text file</param>
        /// <param name="enemies">List of Enemy objects</param>
        public static void LoadFromFile(string filename, List<Enemy> enemies)
        {
            StreamReader reader = null;

            // try-block for reader
            try
            {
                reader = new StreamReader(filename);
                string line = "";

                // splits & adds parts of character data into respected lists
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int damage))
                    {
                        enemies.Add(new Enemy(parts[0], damage));
                    }
                }
            }
            // runs if there is an error
            catch (Exception error)
            {
                Console.WriteLine("Error occurred reading the file: " + error.Message);
            }
            // Close the stream, which closes the file
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
