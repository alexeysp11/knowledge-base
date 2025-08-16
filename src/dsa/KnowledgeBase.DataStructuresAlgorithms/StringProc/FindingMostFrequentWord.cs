//-----------------------------------------------------------------------------
// Finding the most frequent word in text (Intermediate):
// Write an algorithm that analyzes text and returns the most frequent word.
//-----------------------------------------------------------------------------

namespace KnowledgeBase.DataStructuresAlgorithms.StringProc
{
    public static class FindingMostFrequentWord
    {
        public static string Find(string text, IEnumerable<string>? excludeWords = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            string result = string.Empty;

            // Add the number of occurrences of each word in the source string to the dictionary.
            var wordOccuranceDictionary = new Dictionary<string, int>();
            IEnumerable<string> words = GetWordsFromText(text, excludeWords);
            foreach (string word in words)
            {
                if (wordOccuranceDictionary.ContainsKey(word))
                {
                    wordOccuranceDictionary[word] = wordOccuranceDictionary[word] + 1;
                }
                else
                {
                    wordOccuranceDictionary.Add(word, 1);
                }
            }

            // Find the most frequent word.
            int maxNumber = 0;
            foreach (string key in wordOccuranceDictionary.Keys)
            {
                int number = wordOccuranceDictionary[key];
                if (maxNumber < number)
                {
                    maxNumber = number;
                    result = key;
                }
            }

            return result;
        }

        public static IEnumerable<string> GetWordsFromText(string text, IEnumerable<string>? excludeWords = null)
        {
            IEnumerable<string> words = text
                .ToLower()
                .Split(' ')
                .Where(x => excludeWords == null || !excludeWords.Any() || !excludeWords.Contains(x));
            return words;
        }
    }
}
