using KnowledgeBase.DataStructuresAlgorithms.CollectionProc;

namespace KnowledgeBase.DataStructuresAlgorithms.Tests.CollectionProc
{
    public class ObjectFilterByPredicatsTest
    {
        [Fact]
        public void Filter_EmptyList_EmptyList()
        {
            List<string> input = new List<string>();
            List<string> result = ObjectFilterByPredicats.Filter(input, x => true);
            Assert.True(result != null);
            Assert.True(!result.Any());
        }

        [Fact]
        public void Filter_NullList_EmptyList()
        {
            List<string> result = ObjectFilterByPredicats.Filter(null, x => true);
            Assert.True(result != null);
            Assert.True(!result.Any());
        }

        [Fact]
        public void Filter_NotEmptyListAndNotFilter_SameList()
        {
            List<string> input = new List<string> { "Test", "test1234", "1234321" };

            List<string> result = ObjectFilterByPredicats.Filter(input, x => true);

            Assert.True(result != null);
            Assert.True(result.Any());
            Assert.Equal(input.Count, result.Count);
        }
        [Fact]
        public void Filter_NotEmptyListAndFilter_OneElement()
        {
            List<string> input = new List<string> { "Test", "test1234", "1234321" };

            List<string> result = ObjectFilterByPredicats.Filter(input, x => x == "Test");

            Assert.True(result != null);
            Assert.True(result.Any());
            Assert.Equal(1, result.Count);
        }
    }
}
