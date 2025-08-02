using KnowledgeBase.DataStructuresAlgorithms.StringProc;

namespace KnowledgeBase.DataStructuresAlgorithms.Tests.StringProc
{
    public class FindingMostFrequentWordTest
    {
        IEnumerable<string> _excludeWords = ["-", ",", ".", "=", "+"];

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Find_EmptyString_Exception(string input)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                FindingMostFrequentWord.Find(input);
            });
        }

        [Theory]
        [InlineData("Hello hello go go", "hello")]
        [InlineData("Hello go GO, Go", "go")]
        [InlineData("Hello", "hello")]
        public void Find_ValidString_MostFrequentWord(string input, string expected)
        {
            string result = FindingMostFrequentWord.Find(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Hello hello go go", "hello")]
        [InlineData("Hello go GO, Go", "go")]
        [InlineData("Hello", "hello")]
        [InlineData("Hello hello - run", "hello")]
        [InlineData("Hello-hello", "hello-hello")]
        public void Find_ValidStringExcludeWords_MostFrequentWord(string input, string expected)
        {
            string result = FindingMostFrequentWord.Find(input, _excludeWords);
            Assert.Equal(expected, result);
        }
    }
}
