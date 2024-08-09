using System.Collections.Generic; 
using System.Linq; 
using System.Text;

namespace Studying.Leetcode.Trees
{
    /// <summary>
    /// A class for practicing skills in working with a tree (basic operations: initialization, addition, deletion, recalculation).
    /// </summary>
    public class ImplementTree : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// A tree element presented in general form.
        /// </summary>
        public class TreeElement<T>
        {
            #region Public properties
            /// <summary>
            /// Tree element value.
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// The parent element of the current tree element.
            /// </summary>
            public TreeElement<T> Parent { get; private set; }

            /// <summary>
            /// A collection of child elements of the current tree element.
            /// </summary>
            public ICollection<TreeElement<T>> Childs { get; private set; }
            #endregion  // Public properties

            #region Constructors
            /// <summary>
            /// A constructor to initialize a tree element with the following parameters: a value.
            /// </summary>
            public TreeElement(T value) : this(value, null)
            {
            }

            /// <summary>
            /// A constructor to initialize a tree element with the following parameters: a value and a collection of child elements. 
            /// </summary>
            public TreeElement(T value, ICollection<TreeElement<T>> childs) : this(value, null, childs)
            {
            }

            /// <summary>
            /// A constructor to initialize a tree element with the following parameters: a value, parent element 
            /// and a collection of child elements.
            /// </summary>
            public TreeElement(T value, TreeElement<T> parent, ICollection<TreeElement<T>> childs)
            {
                if (childs == null) 
                    childs = new List<TreeElement<T>>();
                if (childs.Any(x => x == null))
                    throw new System.Exception("Specified collection of child elements has an element that is equal to null");
                
                foreach (var child in childs)
                {
                    if (child.Parent == null)
                        child.SetParent(this);
                }
                
                Value = value;
                Parent = parent;
                Childs = childs;
            }
            #endregion  // Constructors

            #region Public methods for a tree structure
            /// <summary>
            /// Method for assigning a parent element to the current tree element.
            /// </summary>
            public void SetParent(TreeElement<T> parent)
            {
                Parent = parent;
            }

            /// <summary>
            /// Method for assigning a child element to the current tree element.
            /// </summary>
            public void AddChild(TreeElement<T> child)
            {
                if (child == null)
                    throw new System.Exception("Cannot add the child element: child element could not be null");
            
                Childs.Add(child);
            }
            #endregion  // Public methods for a tree structure

            #region ToString methods
            /// <summary>
            /// Gets a full representation of a node, including values and childs of it childs, in a following form:  
            /// [value: 0, childs: [value: 1, childs: []], [values: childs: []]]
            /// </summary>
            public override string ToString()
            {
                return $"[value: {Value}, childs: {ToChildRecursiveString()}]".Replace(",]", "]"); 
            }
            
            /// <summary>
            /// Method to display all child elements as a string.
            /// </summary>
            public string ToChildString()
            {
                StringBuilder result = new StringBuilder("[");
                foreach (var child in Childs)
                {
                    result.Append(child.Value).Append(",");
                }
                result.Append("]");
                return result.ToString().Replace(",]", "]");
            }

            /// <summary>
            /// Method to recursively display all child elements as a string.
            /// </summary>
            public string ToChildRecursiveString()
            {
                StringBuilder result = new StringBuilder();
                foreach (var child in Childs)
                {
                    result.Append("[value: ").Append(child.Value).Append(", childs: ");
                    result.Append(child.Childs.Count == 0 ? child.ToChildString() : child.ToChildRecursiveString());
                    result.Append("],");
                }
                return result.ToString().Replace(",]", "]");
            }
            #endregion  // ToString methods
        }

        /// <summary>
        /// A class that represents a tree.
        /// </summary>
        public class Tree<T>
        {
            #region Public properties
            /// <summary>
            /// The root element of a tree.
            /// </summary>
            public TreeElement<T> Root { get; private set; }
            #endregion  // Public properties

            #region Constructors
            /// <summary>
            /// Default constructor.
            /// </summary>
            public Tree() : this(null)
            {
            }

            /// <summary>
            /// A constructor that takes the root element of a tree as an input parameter.
            /// </summary>
            public Tree(TreeElement<T> root)
            {
                Root = root;
            }
            #endregion  // Constructors

            /// <summary>
            /// Method for adding an element to the tree.
            /// </summary>
            public void AddValue(T value)
            {
                if (value == null)
                    throw new System.Exception("Cannot add the specified value: the value could not be null");
                
                if (Root == null)
                {
                    Root = new TreeElement<T>(value);
                    return;
                }
                // Add child elements.
            }

            #region ToString methods
            /// <summary>
            /// Overridden method for displaying a tree as a string.
            /// </summary>
            public override string ToString()
            {
                return Root.ToString(); 
            }
            #endregion  // ToString methods
        }

        /// <summary>
        /// Method to run the example.
        /// </summary>
        public void Execute()
        {
            // Example 1: 
            // input1: [value: 1, childs: [value: 2, childs: [value: 5, childs: []]],[value: 3, childs: []],[value: 4, childs: []]]
            var tree = new Tree<int>(
                new TreeElement<int>(1, new List<TreeElement<int>> 
                {
                    new TreeElement<int>(2, new List<TreeElement<int>> { new TreeElement<int>(5)}),
                    new TreeElement<int>(3),
                    new TreeElement<int>(4)
                })
            );

            System.Console.WriteLine("tree: " + tree.ToString());
        }
    }
}