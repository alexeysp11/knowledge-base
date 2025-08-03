namespace KnowledgeBase.DataStructuresAlgorithms.Sorting
{
    public static class BubbleSorting
    {
        public static List<int> Sort(List<int> inputList)
        {
            if (inputList == null || inputList.Count == 0)
            {
                throw new ArgumentNullException(nameof(inputList));
            }

            List<int> result = new List<int>(inputList);
            int listSize = result.Count;
            bool swapped = false;
            for (int i = 0; i < listSize - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < listSize - i - 1; j++)
                {
                    if (result[j] > result[j + 1])
                    {
                        (result[j], result[j + 1]) = (result[j + 1], result[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break;
                }
            }
            return result;
        }
    }
}
