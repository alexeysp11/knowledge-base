using System.Text;

namespace KnowledgeBase.DataStructuresAlgorithms.StringProc
{
    public static class StringReversal
    {
        public static string Reverse(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var result = new StringBuilder(input.Length);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }
            return result.ToString();
        }
    }
}
