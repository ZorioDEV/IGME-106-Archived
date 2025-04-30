using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_PE
{
    /// <summary>
    /// Graph of a mansion with rooms/vertices & connections/edges.
    /// </summary>
    public class Graph
    {
        // *** FIELDS ***
        private List<Vertex> vertices;
        private Dictionary<string, List<Vertex>> adjacencyList;

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor of the Graph class.
        /// </summary>
        public Graph()
        {
            // adds vertices to the dictionary
            vertices = new List<Vertex>();
            vertices.Add(new Vertex("kitchen", "Large enough to prepare a feast"));
            vertices.Add(new Vertex("dining", "A huge table for sixteen has gold place settings"));
            vertices.Add(new Vertex("library", "This library is packed with floor-to-ceiling bookshelves"));
            vertices.Add(new Vertex("conservatory", "The glass wall allows sunlight to reach the plants here"));
            vertices.Add(new Vertex("hall", "The main hall is central to the house"));
            vertices.Add(new Vertex("deck", "This covered deck looks over the landscaped grounds"));
            vertices.Add(new Vertex("exit", "Cobblestone pathway leads you to the gardens"));

            // initialize adjacency list & manual setup
            adjacencyList = new Dictionary<string, List<Vertex>>();

            adjacencyList["hall"] = new List<Vertex>();
            adjacencyList["hall"].Add(FindVertex("dining"));
            adjacencyList["hall"].Add(FindVertex("conservatory"));
            adjacencyList["hall"].Add(FindVertex("deck"));

            adjacencyList["library"] = new List<Vertex>();
            adjacencyList["library"].Add(FindVertex("kitchen"));
            adjacencyList["library"].Add(FindVertex("conservatory"));

            adjacencyList["dining"] = new List<Vertex>();
            adjacencyList["dining"].Add(FindVertex("kitchen"));
            adjacencyList["dining"].Add(FindVertex("hall"));

            adjacencyList["kitchen"] = new List<Vertex>();
            adjacencyList["kitchen"].Add(FindVertex("dining"));
            adjacencyList["kitchen"].Add(FindVertex("library"));

            adjacencyList["conservatory"] = new List<Vertex>();
            adjacencyList["conservatory"].Add(FindVertex("library"));
            adjacencyList["conservatory"].Add(FindVertex("deck"));
            adjacencyList["conservatory"].Add(FindVertex("hall"));

            adjacencyList["deck"] = new List<Vertex>();
            adjacencyList["deck"].Add(FindVertex("hall"));
            adjacencyList["deck"].Add(FindVertex("conservatory"));
            adjacencyList["deck"].Add(FindVertex("exit"));

            adjacencyList["exit"] = new List<Vertex>();
            adjacencyList["exit"].Add(FindVertex("deck"));
        }

        // *** METHODS ***
        /// <summary>
        /// Lists all vertices in the graph.
        /// </summary>
        public void ListAllVertices()
        {
            foreach (Vertex vertex in vertices)
            {
                Console.WriteLine(vertex.ToString());
            }
        }

        /// <summary>
        /// Checks if the graph contains a room with the specified name.
        /// </summary>
        /// <param name="room">Name of the room to check</param>
        /// <returns>True or False</returns>
        public bool MapContainsRoom(string room)
        {
            foreach (Vertex vertex in vertices)
            {
                if (vertex.Name == room.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if two rooms are adjacent to each other.
        /// </summary>
        /// <param name="firstRoom">Name of the first room</param>
        /// <param name="secondRoom">Name of the second room</param>
        /// <returns>True or False</returns>
        public bool AreAdjacent(string firstRoom, string secondRoom)
        {
            firstRoom = firstRoom.ToLower();
            secondRoom = secondRoom.ToLower();

            if (!adjacencyList.ContainsKey(firstRoom))
            {
                return false;
            }

            foreach (Vertex vertex in adjacencyList[firstRoom])
            {
                if (vertex.Name == secondRoom)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the list of vertices adjacent to the specified room.
        /// </summary>
        /// <param name="room">Name of the specified room</param>
        /// <returns>List of adjacent vertices</returns>
        public List<Vertex> GetAdjacentList(string room)
        {
            room = room.ToLower();

            if (adjacencyList.ContainsKey(room))
            {
                List<Vertex> adjacentVertices = adjacencyList[room];
                if (adjacentVertices != null)
                {
                    return adjacentVertices;
                }
            }

            return null!;
        }

        // *** HELPERS ***
        /// <summary>
        /// Finds vertices by name of a specified room.
        /// </summary>
        /// <param name="name">Name of the specified room</param>
        /// <returns>Vertex of specified room</returns>
        public Vertex FindVertex(string name)
        {
            foreach (Vertex vertex in vertices)
            {
                if (vertex.Name == name)
                {
                    return vertex;
                }
            }
            return null!;
        }
    }
}
