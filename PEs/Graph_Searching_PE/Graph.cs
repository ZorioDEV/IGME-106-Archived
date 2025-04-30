using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Searching_PE
{
    /// <summary>
    /// Graph of a mansion with rooms/vertices & connections/edges.
    /// </summary>
    public class Graph
    {
        // *** FIELDS ***
        private List<Vertex> vertices;
        private Dictionary<string, List<Vertex>> adjacencyList;
        private int[,] adjacencyMatrix;
        private bool[] visited;

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

            // initialize adjacency matrix
            adjacencyMatrix = new int[7, 7]
            {
               // K  D  L  C  H  D  E
                { 0, 1, 1, 0, 0, 0, 0 }, // kitchen
                { 1, 0, 0, 0, 1, 0, 0 }, // dining
                { 1, 0, 0, 1, 0, 0, 0 }, // library
                { 0, 0, 1, 0, 1, 1, 0 }, // conservatory
                { 0, 1, 0, 1, 0, 1, 0 }, // hall
                { 0, 0, 0, 1, 1, 0, 1 }, // deck
                { 0, 0, 0, 0, 0, 1, 0 }  // exit
            };


            // initialize visited array
            visited = new bool[vertices.Count];
        }

        // *** BASE METHODS ***
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

        // *** ALGORITHMS METHODS ***
        /// <summary>
        /// Resets the visited status of all vertices.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }
        }

        /// <summary>
        /// Finds & returns an adjacent unvisited vertex of the specified room.
        /// </summary>
        public Vertex GetAdjacentUnvisited(string roomName)
        {
            int currentIndex = -1;

            // searches for the index of the room name
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Name == roomName.ToLower())
                {
                    currentIndex = i;
                    break;
                }
            }

            // returns null if room not found
            if (currentIndex == -1)
            {
                return null!;
            }

            // looks through adjacency matrix for unvisited connections
            for (int j = 0; j < vertices.Count; j++)
            {
                if (adjacencyMatrix[currentIndex, j] == 1)
                {
                    if (!visited[j])
                    {
                        return vertices[j];
                    }
                }
            }
            return null!;
        }

        /// <summary>
        /// Performs BFS starting from the specified room.
        /// </summary>
        public void BreadthFirst(string roomName)
        {
            roomName = roomName.ToLower();
            if (!MapContainsRoom(roomName))
            {
                Console.WriteLine($"The room {roomName} is not a valid room.");
                return;
            }

            // resets visited
            Reset();
            Vertex start = FindVertex(roomName);
            Queue<Vertex> queue = new Queue<Vertex>();

            // marks start as visited and enqueue
            int startIndex = -1;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i] == start)
                {
                    startIndex = i;
                    break;
                }
            }

            // sets the start vertex as visited
            visited[startIndex] = true;
            Console.WriteLine($"Visited {start.Name}");
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                // peeks front of queue
                Vertex current = queue.Peek();
                Vertex next = GetAdjacentUnvisited(current.Name);

                if (next != null)
                {
                    // marks and enqueue the next adjacent vertex
                    int nextIndex = -1;
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (vertices[i] == next)
                        {
                            nextIndex = i;
                            break;
                        }
                    }

                    // sets the next vertex as visited and adds to queue
                    visited[nextIndex] = true;
                    Console.WriteLine($"Visited {next.Name}");
                    queue.Enqueue(next);
                }
                else
                {
                    // removes current if no unvisited neighbors
                    queue.Dequeue();
                }
            }
        }


        // *** HELPERS METHODS ***
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
