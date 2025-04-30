using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries_PE
{
    /// <summary>
    /// Player information and data
    /// </summary>
    internal class Player
    {
        // *** FEILDS ***
        string name;
        int score;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY properity of the player's name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Read-ONLY properity of the player's score
        /// </summary>
        public int Score
        {
            get
            {
                return score;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Main constructor for the Player.
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="score">Score of the player</param>
        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        // *** METHODS ***
        /// <summary>
        /// Override method of ToString for Player.
        /// </summary>
        /// <returns>String of player's name and score</returns>
        public override string ToString()
        {
            return $"{name}'s score is {score}";
        }

    }
}
