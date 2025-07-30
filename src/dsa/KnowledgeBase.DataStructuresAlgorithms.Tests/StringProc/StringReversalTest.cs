using KnowledgeBase.DataStructuresAlgorithms.StringProc;

namespace KnowledgeBase.DataStructuresAlgorithms.Tests.StringProc
{
    public class StringReversalTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Reverse_EmptyString_EmptyString(string? input)
        {
            string result = StringReversal.Reverse(input);
            Assert.True(string.IsNullOrEmpty(result));
        }

        [Theory]
        [InlineData("hello", "olleh")]
        [InlineData("try", "yrt")]
        [InlineData("12345", "54321")]
        public void Reverse_ValidString_ReversedString(string input, string expected)
        {
            string result = StringReversal.Reverse(input);
            Assert.Equal(expected, result);
        }
    }
}