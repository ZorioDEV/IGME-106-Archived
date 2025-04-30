// Tyler Bye
// 04.07.2025
// Creating A Custom Tree Diagram (with Recursion)

namespace Talent_Tree_PE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // *** VARIABLES ***
            // root node
            TalentTreeNode magic = new TalentTreeNode("Magic", true);

            // left subtree nodes
            TalentTreeNode fireball = new TalentTreeNode("Fireball", true);
            TalentTreeNode crazyBigFireball = new TalentTreeNode("Crazy Big fireball", false);
            TalentTreeNode thousandTinyFireballs = new TalentTreeNode("1000 Tiny fireballs", true);
            TalentTreeNode tinyBurstingFireballs = new TalentTreeNode("Tiny Bursting Fireballs", true);
            TalentTreeNode tinyBouncingFireballs = new TalentTreeNode("Tiny Bouncing Fireballs", false);

            // right subtree nodes
            TalentTreeNode magicArrow = new TalentTreeNode("Magic arrow", true);
            TalentTreeNode iceArrow = new TalentTreeNode("Ice arrow", false);
            TalentTreeNode freezingHeartArrow = new TalentTreeNode("Freezing Heart Arrow", false);
            TalentTreeNode explodingArrow = new TalentTreeNode("Exploding arrow", false);

            // *** MAIN CODE ***
            // assigns the nodes to the proper tree structure
            magic.SetLeftChild(fireball);
            magic.SetRightChild(magicArrow);

            fireball.SetLeftChild(crazyBigFireball);
            fireball.SetRightChild(thousandTinyFireballs);

            thousandTinyFireballs.SetLeftChild(tinyBurstingFireballs);
            thousandTinyFireballs.SetRightChild(tinyBouncingFireballs);

            magicArrow.SetLeftChild(iceArrow);
            magicArrow.SetRightChild(explodingArrow);

            iceArrow.SetRightChild(freezingHeartArrow);

            // prints all talents
            Console.WriteLine("--- Listing all abilities in the game ---\n");
            magic.ListAllTalents();

            // prints known talents
            Console.WriteLine("\n--- Listing all my known abilities ---\n");
            magic.ListKnownTalents();

            // prints possible talents to learn next
            Console.WriteLine("\n--- Listing all abilities I could learn next ---\n");
            magic.ListPossibleTalents();
        }
    }
}
