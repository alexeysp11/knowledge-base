using KnowledgeBase.DataStructuresAlgorithms.StringProc;

namespace KnowledgeBase.DataStructuresAlgorithms.Tests.StringProc
{
    public class AnagramsGroupingTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        public void AreAnagrams_EmptyStrings_True(string input1, string input2)
        {
            bool result = AnagramsGrouping.AreAnagrams(input1, input2);
            Assert.True(result);
        }

        [Theory]
        [InlineData("tea", "eat")]
        [InlineData("tea", "ate")]
        [InlineData("eat", "ate")]
        [InlineData("tan", "nat")]
        [InlineData("listen", "silent")]
        public void AreAnagrams_ValidStrings_True(string input1, string input2)
        {
            bool result = AnagramsGrouping.AreAnagrams(input1, input2);
            Assert.True(result);
        }

        [Theory]
        [InlineData("tea", "number")]
        [InlineData("true", "false")]
        [InlineData("eat", "page")]
        [InlineData("tan", "number")]
        public void AreAnagrams_InvalidStrings_True(string input1, string input2)
        {
            bool result = AnagramsGrouping.AreAnagrams(input1, input2);
            Assert.False(result);
        }

        [Fact]
        public void Group_ValidStrings_ElementsConsistent()
        {
            List<string> input = ["eat", "tea", "tan", "ate", "nat", "bat"];
            List<List<string>> result = AnagramsGrouping.Group(input);

            int count = 0;
            bool elementsConsistent = true;
            foreach (List<string> list in result)
            {
                if (list.Any(x => !input.Contains(x)))
                {
                    elementsConsistent = false;
                    break;
                }
                count += list.Count;
            }

            Assert.Equal(input.Count, count);
            Assert.True(elementsConsistent);
        }
    }
}
