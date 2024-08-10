using System.Collections.Generic; 
using System.Linq; 

namespace KnowledgeBase.Algorithmic.Trees
{
    /// <summary>
    /// A class for practicing skills in working with a binary tree (basic operations: initialization, addition, deletion, recalculation).
    /// </summary>
    /// <ramarks>The following link could be considered a good example of implementing BST: https://gist.github.com/aaronjwood/7e0fc962c5cd898b3181</ramarks>
    public class ImplementBinarySearchTree : KnowledgeBase.Algorithmic.ILeetcodeProblem
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
                string result = "["; 
                foreach (var child in Childs) 
                    result += child.Value + ","; 
                result += "]"; 
                return result.Replace(",]", "]"); 
            }

            /// <summary>
            /// Method to recursively display all child elements as a string.
            /// </summary>
            public string ToChildRecursiveString()
            {
                string result = string.Empty; 
                foreach (var child in Childs) 
                {
                    result += "[value: " + child.Value + ", childs: ";
                    result += child.Childs.Count == 0 ? child.ToChildString() : child.ToChildRecursiveString();
                    result += "],";
                }
                return result.Replace(",]", "]"); 
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

            /// <summary>
            /// A predicate that imposes a condition on adding an element to the tree.
            /// </summary>
            public System.Predicate<T> AddPredicate { get; private set; }
            #endregion  // Public properties

            #region Constructors
            /// <summary>
            /// Default constructor.
            /// </summary>
            public Tree() : this(null, null)
            {
            }

            /// <summary>
            /// A constructor that takes the root element of a tree as an input parameter.
            /// </summary>
            public Tree(TreeElement<T> root) : this(root, null)
            {
            }

            /// <summary>
            /// A constructor that takes the predicate as an input parameter.
            /// </summary>
            public Tree(System.Predicate<T> addPredicate) : this(null, addPredicate)
            {
            }

            /// <summary>
            /// A constructor that takes the root element of the tree and a predicate as input parameters.
            /// </summary>
            public Tree(TreeElement<T> root, System.Predicate<T> addPredicate)
            {
                Root = root;
                AddPredicate = addPredicate == null ? (x => true) : addPredicate;
            }
            #endregion  // Constructors

            // public TreeElement<T> Insert(TreeElement<T> root, T value)
            // {
            //     if (root == null)
            //     {
            //         root = new TreeElement<T>();
            //         root.Value = value;
            //     }
            //     else if (value < root.Value)
            //     {
            //         root.Left = Insert(root.Left, value);
            //     }
            //     else
            //     {
            //         root.right = Insert(root.right, value);
            //     }
            //     return root;
            // }

            /// <summary>
            /// Method for adding an element to the tree.
            /// </summary>
            public void AddValue(T value)
            {
                if (value == null)
                    throw new System.Exception("Cannot add the specified value: the value could not be null");
                if (!AddPredicate(value))
                    throw new System.Exception("Cannot add the specified value: the value violates predicate condition");
                
                if (Root == null)
                {
                    Root = new TreeElement<T>(value);
                    return;
                }

                // Since this is a binary search tree, we start traversing the tree.
                // First we take the root element and compare it with the value:
                // - if the value is greater, then look at the second child element;
                // - if the value is less, then look at the first child element.
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

            // var bstPredicate = x => true;

            System.Console.WriteLine("tree: " + tree.ToString());
        }
    }
}