using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dynamic_Tree_PE
{
    /// <summary>
    /// Represents a tree-centric data structure
    /// that can have data dynamically inserted, 
    /// and can be drawn as a literal "tree" on the screen
    /// </summary>
    class Tree : DrawableTree
    {
        // Already has an inherited root node field called "root"

        /// <summary>
        /// Creates a tree that can be drawn
        /// </summary>
        /// <param name="sb">The sprite batch used to draw</param>
        /// <param name="treeColor">The color of this tree</param>
        public Tree(SpriteBatch sb, Color treeColor)
            : base(sb, treeColor)
        { }

        /// <summary>
        /// Public facing Add method
        /// </summary>
        /// <param name="data">The data to add</param>
        public void Add(int data)
        {
            // checks if root exists & creates one with the given data
            if (root == null)
            {
                root = new TreeNode(data);
            }
            // recursively adds from the root
            else
            {
                Add(data, root);
            }
        }

        /// <summary>
        /// Private recursive Add method
        /// </summary>
        /// <param name="data">The data to add</param>
        /// <param name="node">The node to attempt to add into</param>
        private void Add(int data, TreeNode node)
        {
            // checks if data is less than current node, then goes left
            if (data < node.Data)
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode(data);
                }
                else
                {
                    Add(data, node.Left);
                }
            }
            // if data is greater than or equal, then goes right
            else
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode(data);
                }
                else
                {
                    Add(data, node.Right);
                }
            }
        }
    }
}
