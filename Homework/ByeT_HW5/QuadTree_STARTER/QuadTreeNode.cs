using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace QuadTree_STARTER
{
	class QuadTreeNode
	{
		// The maximum number of objects in a quad
		// BEFORE a subdivision occurs.  In other words,
		// once this node has MORE than this many objects,
		// it should divide.
		private const int MaxObjectsBeforeSubdivide = 3;

		/// <summary>
		/// The 4 divisions of this quad
		/// </summary>
		public QuadTreeNode[] Divisions { get; private set; }

		/// <summary>
		/// This quad's rectangular area
		/// </summary>
		public Rectangle Bounds { get; private set; }

		/// <summary>
		/// The game objects (colored rectangles) inside this quad
		/// </summary>
		public List<GameObject> GameObjects { get; private set; }

		/// <summary>
		/// Creates a new Quad Tree Node
		/// </summary>
		/// <param name="x">This quad's x position</param>
		/// <param name="y">This quad's y position</param>
		/// <param name="width">This quad's width</param>
		/// <param name="height">This quad's height</param>
		public QuadTreeNode(int x, int y, int width, int height)
		{
			// Save the rectangle
			Bounds = new Rectangle(x, y, width, height);

			// Create the object list
			GameObjects = new List<GameObject>();

			// No divisions yet
			Divisions = null;
		}


		/// <summary>
		/// Adds a game object to the best fitting quad's list of GameObjects.  
		/// If the quad has too many objects in it, and hasn't been divided
		/// already, it divides into four children.
		/// </summary>
		/// <param name="gameObj">The object to add</param>
		public bool AddObject(GameObject gameObj)
		{
            // ---------------------------------------------------------
            // Questions to consider / Algorithm:
            //  - Does it not fit in this quad? If so, don't add it here.
            //  - Does this quad have children?
            //  - Does the GameObject fit in one of those children?
            //    Add this GameObject to that child quad.
            //  - The GameObject doesn't fit in one of the children?
            //    Add it here at the parent quad.
            //  - This quad doesn't have children?
            //    Then the GameObject must be added to this quad.
            // ---------------------------------------------------------

            // checks if the object fits in this quad
            if (!Bounds.Contains(gameObj.Rectangle))
            {
                return false;
            }

            // checks if this quad has divisions
            if (Divisions != null)
            {
                // tries to add the object to a child quad
                foreach (QuadTreeNode child in Divisions)
                {
                    if (child.AddObject(gameObj))
                    {
                        return true;
                    }
                }
            }

            // adds the object to this quad
            GameObjects.Add(gameObj);

            // checks if we need to divide
            if (GameObjects.Count > MaxObjectsBeforeSubdivide && Divisions == null)
            {
                Divide();
            }

            return true;
        }

		/// <summary>
		/// Divides this quad into 4 smaller quads.  Moves any game objects
		/// that are completely contained within the new smaller quads into
		/// those quads and removes them from this one.
		/// </summary>
		public void Divide()
		{
            // ---------------------------------------------------------
            // Questions to consider / Algorithm:
            //  - Perform division.
            //  - Determine the calculation of each 4 child node's bounds.
            //  - Do any GameObjects in the parent node fit within one of
            //    these 4 children?
            //  - Yes? Add this GameObject to the best fitting child quad
            //    and remove it from the parent's list.
            //  - No? Leave the GameObject in the parent's list.
            // ----------------------------------------------------------

            // divides only if not already divided
            if (Divisions == null)
            {
                int halfWidth = Bounds.Width / 2;
                int halfHeight = Bounds.Height / 2;
                int midX = Bounds.X + halfWidth;
                int midY = Bounds.Y + halfHeight;

                // creates 4 new child quads
                Divisions = new QuadTreeNode[4];
                Divisions[0] = new QuadTreeNode(Bounds.X, Bounds.Y, halfWidth, halfHeight); // top-left
                Divisions[1] = new QuadTreeNode(midX, Bounds.Y, halfWidth, halfHeight);    // top-right
                Divisions[2] = new QuadTreeNode(Bounds.X, midY, halfWidth, halfHeight);    // bottom-left
                Divisions[3] = new QuadTreeNode(midX, midY, halfWidth, halfHeight);        // bottom-right

                // moves objects to child quads if they fit
                for (int i = GameObjects.Count - 1; i >= 0; i--)
                {
                    GameObject obj = GameObjects[i];

                    // tries to add the object to a child quad
                    bool moved = false;
                    foreach (QuadTreeNode child in Divisions)
                    {
                        if (child.Bounds.Contains(obj.Rectangle))
                        {
                            child.AddObject(obj);
                            GameObjects.RemoveAt(i);
                            moved = true;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recursively populates a list with all of the bounding rectangles
        /// in the tree.  This include the bounding rectangle for this quad and 
        /// all child quads (if they exist).  Use the "AddRange" method of
        /// the list class to add the elements from one list to another.
        /// </summary>
        /// <returns>A list of rectangles representing all of the quads in the tree</returns>
        public List<Rectangle> GetAllQuadBounds()
		{
			// DO NOT MODIFY THIS STATEMENT:
			List<Rectangle> rects = new List<Rectangle>();

            // adds this quad's bounds
            rects.Add(Bounds);

            // recursively adds child quads if they exist
            if (Divisions != null)
            {
                foreach (QuadTreeNode child in Divisions)
                {
                    rects.AddRange(child.GetAllQuadBounds());
                }
            }

            // DO NOT MODIFY THIS STATEMENT:
            return rects;
		}

		/// <summary>
		/// A possibly recursive method that returns the
		/// smallest quad that contains the specified rectangle
		/// </summary>
		/// <param name="gameObjectRect">A GameObject's rectangle to check</param>
		/// <returns>The smallest quad that contains the rectangle</returns>
		public QuadTreeNode GetSmallestContainingQuad(Rectangle gameObjectRect)
		{
            // ----------------------------------------------------------
            // Questions to consider / Algorithm:
            //  - Find the smallest quad that this GameObject fit in,
            //    then return that quad node.
            //  - Doesn't fit in this parent or any of its children?
            //    Return null.
            // ----------------------------------------------------------

            // checks if this quad contains the rectangle
            if (!Bounds.Contains(gameObjectRect))
            {
                return null;
            }

            // checks child quads
            if (Divisions != null)
            {
                foreach (QuadTreeNode child in Divisions)
                {
                    QuadTreeNode containingQuad = child.GetSmallestContainingQuad(gameObjectRect);
                    if (containingQuad != null)
                    {
                        return containingQuad;
                    }
                }
            }

            // returns this quad if no children contain the rectangle
            return this;
        }
	}
}
