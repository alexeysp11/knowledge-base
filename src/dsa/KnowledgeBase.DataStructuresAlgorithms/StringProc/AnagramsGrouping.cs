//-----------------------------------------------------------------------------
// 1. Anagram Grouping (Intermediate):
// Write an algorithm that takes a list of strings and groups the anagrams into separate lists.
// For example, `["eat", "tea", "tan", "ate", "nat", "bat"]` -> `[["eat", "tea", "ate"], ["tan", "nat"], ["bat"]]`.

// 2. Anagram Finding (Intermediate):
// Write an algorithm that determines whether two strings are anagrams
// (contain the same characters in different orders).
// For example, "listen" and "silent".
//-----------------------------------------------------------------------------

namespace KnowledgeBase.DataStructuresAlgorithms.StringProc
{
    public static class AnagramsGrouping
    {
        public static List<List<string>> Group(List<string> inputList)
        {
            if (inputList == null || !inputList.Any())
            {
                throw new Exception($"Argument '{nameof(inputList)}' does not contain any element");
            }

            int maxGroupNumber = 0;
            var anagramGroupDictionary = new Dictionary<string, int>();
            foreach (string inputString1 in inputList)
            {
                if (maxGroupNumber == 0)
                {
                    anagramGroupDictionary.Add(inputString1, maxGroupNumber);
                    maxGroupNumber += 1;
                }
                foreach (string inputString2 in inputList)
                {
                    if (inputString1 == inputString2)
                    {
                        continue;
                    }
                    if (anagramGroupDictionary.ContainsKey(inputString2))
                    {
                        continue;
                    }
                    if (AreAnagrams(inputString1, inputString2))
                    {
                        if (!anagramGroupDictionary.ContainsKey(inputString1))
                        {
                            anagramGroupDictionary.Add(inputString2, maxGroupNumber);
                            maxGroupNumber += 1;
                            continue;
                        }
                        else
                        {
                            anagramGroupDictionary.Add(inputString2, anagramGroupDictionary[inputString1]);
                        }
                    }
                }
            }

            // If the number of elements in dictionary is not equal to input list, then add the elements.
            if (inputList.Count != anagramGroupDictionary.Count)
            {
                List<string> elements = inputList.Where(x => !anagramGroupDictionary.ContainsKey(x)).ToList();
                foreach (string element in elements)
                {
                    anagramGroupDictionary.Add(element, maxGroupNumber);
                    maxGroupNumber += 1;
                }
            }

            var result = new List<List<string>>();
            for (int i = 0; i < maxGroupNumber; i++)
            {
                result.Add(new List<string>());
            }
            foreach (string key in anagramGroupDictionary.Keys)
            {
                int groupNumber = anagramGroupDictionary[key];
                result[groupNumber].Add(key);
            }
            return result;
        }

        public static bool AreAnagrams(string input1,  string input2)
        {
            if (string.IsNullOrEmpty(input1) && string.IsNullOrEmpty(input2))
            {
                return true;
            }
            if ((input1?.Length ?? 0) != (input2?.Length ?? 0))
            {
                return false;
            }

            Dictionary<char, int> dictionary1 = GetDictionaryByInputString(input1);
            Dictionary<char, int> dictionary2 = GetDictionaryByInputString(input2);
            foreach (var key1 in dictionary1.Keys)
            {
                if (!dictionary2.ContainsKey(key1))
                {
                    return false;
                }
                if (dictionary1[key1] != dictionary2[key1])
                {
                    return false;
                }
            }

            return true;
        }

        private static Dictionary<char, int> GetDictionaryByInputString(string input)
        {
            var dictionary = new Dictionary<char, int>();
            foreach (char ch in input)
            {
                if (dictionary.ContainsKey(ch))
                {
                    dictionary[ch] = dictionary[ch] + 1;
                }
                else
                {
                    dictionary[ch] = 1;
                }
            }
            return dictionary;
        }
    }
}
