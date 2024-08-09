using System.Collections.Generic; 
using System.Linq; 

namespace Studying.Leetcode.Trees
{
    /// <summary>
    /// Problem description: https://leetcode.com/problems/balanced-binary-tree.
    /// </summary>
    public class BalancedBinaryTree : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// Represents a node in a binary tree.
        /// </summary>
        internal class TreeNode
        {
            /// <summary>
            /// Gets or sets the value of the node.
            /// </summary>
            internal int val;

            /// <summary>
            /// Gets or sets the left child of the node.
            /// </summary>
            internal TreeNode left;

            /// <summary>
            /// Gets or sets the right child of the node.
            /// </summary>
            internal TreeNode right;

            /// <summary>
            /// Initializes a new instance of the TreeNode class with specified value, left child, and right child.
            /// </summary>
            /// <param name="val">The value of the node.</param>
            /// <param name="left">The left child of the node.</param>
            /// <param name="right">The right child of the node.</param>
            internal TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        /// <summary>
        /// Method to run the example.
        /// </summary>
        public void Execute()
        {
            // Given a binary tree, determine if it is height-balanced.
            // A height-balanced binary tree is a binary tree in which the depth of the two subtrees 
            // of every node never differs by more than one.

            // Example 1:
            // Input: root = [3,9,20,null,null,15,7]
            // Output: true
            var input01 = new List<int?> {3,9,20,null,null,15,7};
            var root01 = InitializeBalancedTree(input01);
            var height01 = GetHeight(root01);
            var result01 = IsBalanced(root01);
            System.Console.WriteLine("height01: " + height01);
            System.Console.WriteLine("result01: " + result01);
            PrintBinaryTree(root01);

            // Example 2:
            // Input: root = [1,2,2,3,3,null,null,4,4]
            // Output: false
            var input02 = new List<int?> {1,2,2,3,3,null,null,4,4};
            var root02 = InitializeBalancedTree(input02);
            var height02 = GetHeight(root02);
            var result02 = IsBalanced(root02);
            System.Console.WriteLine("height02: " + height02);
            System.Console.WriteLine("result02: " + result02);
            PrintBinaryTree(root02);

            // Example 3:
            // Input: root = []
            // Output: true
            var input03 = new List<int?> {};
            var root03 = InitializeBalancedTree(input03);
            var height03 = GetHeight(root03);
            var result03 = IsBalanced(root03);
            System.Console.WriteLine("height03: " + height03);
            System.Console.WriteLine("result03: " + result03);
            PrintBinaryTree(root03);
        }

        /// <summary>
        /// Initializes a balanced binary tree using the elements from the input array.
        /// </summary>
        /// <param name="array">The input array containing integer values.</param>
        private TreeNode InitializeBalancedTree(List<int?> array)
        {
            if (array == null || array.Count == 0)
            {
                return null;
            }

            var sortedArray = array.Where(x => x != null).OrderBy(x => x).ToList();
            return ConstructBalancedTree(sortedArray, 0, sortedArray.Count - 1);
        }

        /// <summary>
        /// A recursive function that will build a tree in a balanced way, starting from the middle of the array.
        /// </summary>
        private TreeNode ConstructBalancedTree(List<int?> array, int leftIndex, int rightIndex)
        {
            if (leftIndex > rightIndex)
            {
                return null;
            }

            int midIndex = leftIndex + (rightIndex - leftIndex) / 2;
            TreeNode node = new TreeNode(array[midIndex].HasValue ? array[midIndex].Value : 0);

            node.left = ConstructBalancedTree(array, leftIndex, midIndex - 1);
            node.right = ConstructBalancedTree(array, midIndex + 1, rightIndex);

            return node;
        }

        /// <summary>
        /// Checks if the binary tree is height-balanced.
        /// </summary>
        private bool IsBalanced(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            int leftHeight = GetHeight(root.left);
            int rightHeight = GetHeight(root.right);

            if (System.Math.Abs(leftHeight - rightHeight) <= 1 && IsBalanced(root.left) && IsBalanced(root.right))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the height of the tree node.
        /// </summary>
        private int GetHeight(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + System.Math.Max(GetHeight(node.left), GetHeight(node.right));
        }

        private void PrintBinaryTree(TreeNode node)
        {
            System.Console.WriteLine("Printing Tree Node:");
            PrintHelper(node, 0);
        }

        private void PrintHelper(TreeNode node, int indent)
        {
            if (node == null)
                return;

            // Print the current node with proper indentation
            System.Console.WriteLine($"{new string(' ', indent)}{node.val}");

            // Recursively print the left and right children with increased indentation
            PrintHelper(node.left, indent + 4);
            PrintHelper(node.right, indent + 4);
        }
    }
}