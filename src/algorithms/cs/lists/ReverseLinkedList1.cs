using System.Linq;

namespace Studying.Leetcode.Lists
{
    /// <summary>
    /// Reverse a linked list
    /// </summary>
    public class ReverseLinkedList1 : Studying.Leetcode.ILeetcodeProblem
    {
        public class LinkedListNode
        {
            public int Value { get; set; }
            public LinkedListNode Next { get; set; }
        }

        public class LinkedList
        {
            public LinkedListNode Root { get; set; }
            public int Count { get; set; }
        }

        public void Execute()
        {
            // Input: Head of following linked list 
            // 1->2->3->4->NULL 
            // Output: Linked list should be changed to, 
            // 4->3->2->1->NULL
            var elements01 = new int?[] {1,2,3,4,null};
            var list01 = Initialize(elements01);
            var result01 = Reverse(list01);
            PrintElements(elements01);
            PrintElements(result01.Cast<int?>().ToArray());

            // Input: Head of following linked list 
            // 1->2->3->4->5->NULL 
            // Output: Linked list should be changed to, 
            // 5->4->3->2->1->NULL
            var elements02 = new int?[] {1,2,3,4,5,null};
            var list02 = Initialize(elements02);
            var result02 = Reverse(list02);
            PrintElements(elements02);
            PrintElements(result02.Cast<int?>().ToArray());
        }

        private LinkedList Initialize(int?[] values)
        {
            var list = new LinkedList();
            if (values == null || values.Length == 0)
                return list;
            var count = 0;
            var root = new LinkedListNode();
            var previous = new LinkedListNode();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                    continue;
                var node = new LinkedListNode
                {
                    Value = (int)values[i]
                };
                if (i == 0)
                    root = node;
                else
                    previous.Next = node;
                previous = node;
                count += 1;
            }
            list.Root = root;
            list.Count = count;
            return list;
        }

        private int[] Reverse(LinkedList list)
        {
            if (list == null || list.Count == 0)
                throw new System.Exception("List could not be null or empty");
            var tmp = new int[list.Count];
            var result = new int[list.Count];
            var current = list.Root;
            var index = 0;
            while (current != null)
            {
                tmp[index] = current.Value;
                current = current.Next;
                index += 1;
            }
            int j = 0;
            for (int i = tmp.Length - 1; i >= 0; i--)
            {
                result[j] = tmp[i];
                j += 1;
            }
            return result;
        }

        private void PrintElements(int?[] elements)
        {
            System.Console.Write("[");
            for (int i = 0; i < elements.Length; i++)
            {
                System.Console.Write((elements[i] == null ? "NULL" : ((int)elements[i]).ToString()));
                System.Console.Write((i == elements.Length - 1 ? "" : " "));
            }
            System.Console.WriteLine("]");

        }
    }
}