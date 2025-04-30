using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByeT_HW1
{
    /// <summary>
    /// Object for the board's data.
    /// </summary>
    internal class Cell
    {
        // *** FIELDS ***
        private bool isAlive;

        // *** PROPERTIES ***
        /// <summary>
        /// Read and Write-property for isAlive.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Main constructor for Cell object.
        /// </summary>
        /// <param name="isAlive">Bool for if the cell is dead or alive</param>
        public Cell(bool isAlive)
        {
            this.isAlive = isAlive;
        }

        // *** METHODS ***
        /// <summary>
        /// Default characters for printing to console.
        /// </summary>
        /// <returns>Defualt characters</returns>
        public override string ToString()
        {
            // tests if cell is alive or not
            if(isAlive)
            {
                return "o";
            }
            else
            {
                return "x";
            }
        }

        /// <summary>
        /// Custom characters for printing to console.
        /// </summary>
        /// <param name="aliveChar">Custom alive character</param>
        /// <param name="deadChar">Custom dead character</param>
        /// <returns>Custom characters</returns>
        public string CustomChar(char aliveChar, char deadChar)
        {
            // tests if cell is alive or not
            if (isAlive)
            {
                return $"{aliveChar}";
            }
            else
            {
                return $"{deadChar}";
            }
        }
    }
}
