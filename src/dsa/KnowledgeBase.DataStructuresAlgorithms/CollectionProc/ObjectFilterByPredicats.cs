//-----------------------------------------------------------------------------
// Implementing a simple list filter (simple):
// Write an algorithm that takes a list of objects and a predicate (`Func<T, bool>`)
// and returns a new list containing only the elements that satisfy the predicate.
//-----------------------------------------------------------------------------

namespace KnowledgeBase.DataStructuresAlgorithms.CollectionProc
{
    public static class ObjectFilterByPredicats
    {
        public static List<string> Filter(List<string> inputList, Func<string, bool> func)
        {
            if (inputList == null || !inputList.Any())
            {
                return new List<string>();
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), "Predicate could not be null");
            }

            var result = new List<string>();
            foreach (string item in inputList)
            {
                if (func(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
