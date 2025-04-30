using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW1
{
    /// <summary>
    /// Object for the game's data and logic.
    /// </summary>
    internal class Game
    {
        // *** FIELDS ***
        private Cell[,] board;
        private Cell[,] futureBoard;

        private int width;
        private int height;

        private int percentage = 30;

        private char aliveChar = 'o';
        private char deadChar = 'x';

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY bool property of if a board exists.
        /// </summary>
        public bool HasBoard
        {
            get
            {
                // test if a board is made or not
                if(board != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Main constructor of the Game object.
        /// </summary>
        public Game()
        {
            
        }

        // *** METHODS ***
        /// <summary>
        /// Generates a random sized board and random life spawn.
        /// </summary>
        public void GenerateBoard()
        {
            // resets characters to default
            aliveChar = 'o';
            deadChar = 'x';

            // randomly generates board size
            Random rand = new Random();
            width = rand.Next(5, 20);
            height = rand.Next(5, 15);
            board = new Cell[height, width];

            // assigns values to the board
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // 30% chance of being alive
                    board[i, j] = new Cell(rand.Next(0, 100) <= percentage);
                }
            }

            // tells users the board size
            Console.WriteLine($"A {width}x{height} board was generated.\n");
        }

        /// <summary>
        /// Prints the board to the console.
        /// </summary>
        public void DisplayBoard()
        {
            // tests if using default characters or not
            if (aliveChar != 'o' && deadChar != 'x')
            {
                // loops through all indexes & prints with custom characters from file
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (board[i, j].IsAlive)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(board[i, j].CustomChar(aliveChar, deadChar));
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(board[i, j].CustomChar(aliveChar, deadChar));
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                // loops through all indexes & prints with defualt characters
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (board[i, j].IsAlive)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(board[i, j].ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(board[i, j].ToString());
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Loads a pre-made board from a text file.
        /// </summary>
        /// <param name="filename">Name of the text file.</param>
        public void LoadBoard(string filename)
        {
            StreamReader reader = null!;

            // try-block for reader
            try
            {
                reader = new StreamReader(filename);
                string line;

                // reads and assigns width and height variables
                line = reader.ReadLine()!;
                string[] dimensions = line.Split(',');
                width = int.Parse(dimensions[0]);
                height = int.Parse(dimensions[1]);

                // reads and assigns custom character variables
                line = reader.ReadLine()!;
                string[] characters = line.Split(',');
                aliveChar = char.Parse(characters[0]);
                deadChar = char.Parse(characters[1]);

                // array of all the board's lines
                string[] lines = new string[height];

                // reads and assigns each line to the array
                for (int i = 0; i < height; i++)
                {
                    line = reader.ReadLine()!;
                    lines[i] = line;
                }

                // give the board the dead or alive data for all indexes
                board = new Cell[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if(lines[i][j] == 'o')
                        {
                            board[i, j] = new Cell(true);
                        }
                        else
                        {
                            board[i, j] = new Cell(false);
                        }
                    }
                }

                // prints out custom board
                DisplayBoard();
            }
            // runs if there is an error
            catch (Exception error)
            {
                Console.WriteLine("Error occurred reading the file: " + error.Message);
            }
            // close the stream, which closes the file
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Advances the board life by one cycle.
        /// </summary>
        public void AdvanceBoard()
        {
            futureBoard = new Cell[height, width];

            // tests if the previous board's cells will be alive next cycle
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(i, j);
                    bool isAlive = board[i, j].IsAlive;

                    // tests if there are less than 2 or greater than 3 alive neighbors
                    if (isAlive && (liveNeighbors < 2 || liveNeighbors > 3))
                    {
                        // cell dies
                        futureBoard[i, j] = new Cell(false);
                    }
                    // tests if cell is dead & if it has 3 alive neighbors
                    else if (!isAlive && liveNeighbors == 3)
                    {
                        // cell becomes alive
                        futureBoard[i, j] = new Cell(true);
                    }
                    // if doesn't fall under any other requirements, then cell stays alive
                    else
                    {
                        // cell remains alive
                        futureBoard[i, j] = new Cell(isAlive);
                    }
                }
            }

            // copies futureBoard state to board
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j].IsAlive = futureBoard[i, j].IsAlive;
                }
            }
        }

        /// <summary>
        /// Counts the number of alive cells around a certain cell.
        /// </summary>
        /// <param name="row">Row of the cell</param>
        /// <param name="col">Column of the cell</param>
        /// <returns>Count of alive neighbors</returns>
        private int CountLiveNeighbors(int row, int col)
        {
            int count = 0;
            int[] directions = { 
                -1,     // up or left
                0,      // no movement
                1       // down or right
            };

            // loops through all directions in the rows and columns
            foreach (int directionRow in directions)
            {
                foreach (int directionCol in directions)
                {
                    // doesn't account for selected cell
                    if (directionRow == 0 && directionCol == 0)
                    {
                        continue;
                    }

                    int newRow = row + directionCol;
                    int newCol = col + directionRow;

                    // accounts for edges and out-of-bounds
                    if (newRow >= 0 && 
                        newRow < height && 
                        newCol >= 0 && 
                        newCol < width)
                    {
                        // adds to the count if alive
                        if (board[newRow, newCol].IsAlive)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Saves the current board to a new file.
        /// </summary>
        /// <param name="filename">Name of the new file</param>
        public void SaveBoard(string filename)
        {
            StreamWriter writer = null!;
            try
            {
                writer = new StreamWriter(filename);

                // writes board dimensions
                writer.WriteLine($"{width},{height}");

                // writes custom alive and dead characters
                writer.WriteLine($"{aliveChar},{deadChar}");

                // writes the board state
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        writer.Write(board[i, j]);
                    }
                    writer.WriteLine();
                }
            }
            // runs if there is an error
            catch (Exception error)
            {
                Console.WriteLine("Error occurred while saving the file: " + error.Message);
            }
            // close the stream, which closes the file
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
