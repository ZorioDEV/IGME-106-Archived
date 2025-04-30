using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_Algo_PE
{
    /// <summary>
    /// Represents a room (vertex) in the mansion with a name and description.
    /// </summary>
    public class Vertex
    {
        // *** FIELDS ***
        private string name;
        private string description;
        private int distanceFromSource;
        private Vertex nearestVertex;
        private bool isPermanent;

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

        /// <summary>
        /// Read and write property of the distance from the source vertex.
        /// </summary>
        public int DistanceFromSource
        {
            get
            { 
                return distanceFromSource; 
            }
            set 
            { 
                distanceFromSource = value; 
            }
        }

        /// <summary>
        /// Read and write property of the nearest vertex in the shortest path.
        /// </summary>
        public Vertex NearestVertex
        {
            get 
            { 
                return nearestVertex; 
            }
            set 
            { 
                nearestVertex = value; 
            }
        }

        /// <summary>
        /// Read and write property of whether the vertex is permanent in Dijkstra's algorithm.
        /// </summary>
        public bool IsPermanent
        {
            get 
            { 
                return isPermanent; 
            }
            set 
            { 
                isPermanent = value; 
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
            this.distanceFromSource = int.MaxValue;
            this.nearestVertex = null!;
            this.isPermanent = false;
        }

        // *** METHODS ***
        /// <summary>
        /// Resets the vertex's data Dijkstra's algorithm
        /// </summary>
        public void ResetDijkstraData()
        {
            distanceFromSource = int.MaxValue;
            nearestVertex = null!;
            isPermanent = false;
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
