using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Searching_PE
{
    /// <summary>
    /// Represents a room (vertex) in the mansion with a name and description.
    /// </summary>
    public class Vertex
    {
        // *** FIELDS ***
        private string name;
        private string description;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY property of the name of the room.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Read-ONLY property of the description of the room.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
        }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor of the Vertex class.
        /// </summary>
        /// <param name="name">Name of the room</param>
        /// <param name="description">Description of the room</param>
        public Vertex(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        // *** OVERRIDE ***
        /// <summary>
        /// Overrides the string format of the vertex.
        /// </summary>
        /// <returns>Formatted string with room name & description</returns>
        public override string ToString()
        {
            return $"{name.ToUpper()}: {description}.";
        }
    }
}
