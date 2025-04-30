using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talent_Tree_PE
{
    public class TalentTreeNode
    {
        // *** PROPERTIES ***
        /// <summary>
        /// READ and WRITE Property for the ability's name.
        /// </summary>
        public string AbilityName { get; set; }

        /// <summary>
        /// READ and WRITE Property for if the player has learned the ability or not.
        /// </summary>
        public bool Learned { get; set; }

        /// <summary>
        /// READ and WRITE Property for node's left child info.
        /// </summary>
        public TalentTreeNode LeftChild { get; set; }

        /// <summary>
        /// READ and WRITE Property for node's right child info.
        /// </summary>
        public TalentTreeNode RightChild { get; set; }

        // *** CONSTRUCTOR ***
        /// <summary>
        /// Main constructor of the specific node.
        /// </summary>
        /// <param name="abilityName">Name of the node's ability</param>
        /// <param name="learned">If the player has learned the ability or not</param>
        public TalentTreeNode(string abilityName, bool learned)
        {
            AbilityName = abilityName;
            Learned = learned;
            LeftChild = null!;
            RightChild = null!;
        }

        // *** METHODS ***
        /// <summary>
        /// Method to set left child.
        /// </summary>
        /// <param name="left">Node that is being set to the left child</param>
        public void SetLeftChild(TalentTreeNode left)
        {
            LeftChild = left;
        }

        /// <summary>
        /// Method to set right child.
        /// </summary>
        /// <param name="right">Node that is being set to the right child</param>
        public void SetRightChild(TalentTreeNode right)
        {
            RightChild = right;
        }

        /// <summary>
        /// Traverses in order to list all talents, using recursion.
        /// </summary>
        public void ListAllTalents()
        {
            // recursively traverses left child if it exists
            if (LeftChild != null)
            {
                LeftChild.ListAllTalents();
            }

            // prints current node's ability
            Console.WriteLine(AbilityName);

            // recursively traverses right child if it exists
            if (RightChild != null)
            {
                RightChild.ListAllTalents();
            }
        }

        /// <summary>
        /// Traverses pre-order to list all known talents, using recursion.
        /// </summary>
        public void ListKnownTalents()
        {
            // checks if this talent is learned
            if (Learned)
            {
                Console.WriteLine($"Known ability: {AbilityName}");

                // recursively checks left child
                if (LeftChild != null)
                {
                    LeftChild.ListKnownTalents();
                }

                // recursively checks right child
                if (RightChild != null)
                {
                    RightChild.ListKnownTalents();
                }
            }
        }

        /// <summary>
        /// Lists all possible talents that can be learned next.
        /// </summary>
        public void ListPossibleTalents()
        {
            // checks if left child exists
            if (LeftChild != null)
            {
                // checks if current node is learned & left child isn't
                if (Learned && !LeftChild.Learned)
                {
                    Console.WriteLine($"Possible ability: {LeftChild.AbilityName}");
                }

                // recursively checks left child's children
                LeftChild.ListPossibleTalents();
            }

            // checks if right child exists
            if (RightChild != null)
            {
                // checks if current node is learned & right child isn't
                if (Learned && !RightChild.Learned)
                {
                    Console.WriteLine($"Possible ability: {RightChild.AbilityName}");
                }

                // recursively checks right child's children
                RightChild.ListPossibleTalents();
            }
        }
    }
}
