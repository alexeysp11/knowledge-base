using KnowledgeBase.DataStructuresAlgorithms.Sorting;

namespace KnowledgeBase.DataStructuresAlgorithms.Tests.Sorting
{
    public class BubbleSortingTest
    {
        List<int> _emptyInputList;
        List<int> _validInputList;
        List<int> _validSortedList;

        public BubbleSortingTest()
        {
            _emptyInputList = new List<int>();
            _validInputList = new List<int> { 9, 5, 7, 6, 2, 1, 4, 3, 8 };
            _validSortedList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        [Fact]
        public void Sort_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => BubbleSorting.Sort(null));
        }

        [Fact]
        public void Sort_EmptyInputList_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => BubbleSorting.Sort(_emptyInputList));
        }

        [Fact]
        public void Sort_ValidInputList_ValidSortedList()
        {
            List<int> result = BubbleSorting.Sort(_validInputList);
            Assert.Equal(_validSortedList.Count, result.Count);
            Assert.True(CompareTwoCollections(_validSortedList, result));
        }

        private bool CompareTwoCollections(List<int> input1, List<int> input2)
        {
            if ((input1?.Count ?? 0) == 0 && (input2?.Count ?? 0) == 0)
            {
                return true;
            }
            if ((input1?.Count ?? 0) != (input2?.Count ?? 0))
            {
                return false;
            }
            for (int i = 0; i < input1.Count; i++)
            {
                if (input1[i] != input2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
