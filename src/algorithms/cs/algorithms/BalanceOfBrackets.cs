using System.Collections.Generic;
using System.Linq;

namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    public class BalanceOfBrackets : Studying.Leetcode.ILeetcodeProblem
    {
        public void Execute()
        {
            // 
            System.Console.WriteLine(IsBalanced("()[][{}]"));   // true
            System.Console.WriteLine(IsBalanced("[}"));         // false
            System.Console.WriteLine(IsBalanced("})[]{}"));     // false
            System.Console.WriteLine(IsBalanced("(}"));         // false
            System.Console.WriteLine(IsBalanced("({https://google.com})"));     // true
            System.Console.WriteLine(IsBalanced("{}a"));        // true
        }

        private bool IsBalanced(string text)
        {
            // An array of allowed symbols (all opening and closing parentheses).
            var allowedSymbols = new Dictionary<char, char>();
            allowedSymbols.Add('{', '}');
            allowedSymbols.Add('(', ')');
            allowedSymbols.Add('[', ']');

            // In order to store information on open/closed parentheses, let's create a dictionary <char, int>.
            var symbols = new Dictionary<char, int>();

            // Fill the dictionary with allowed symbols.
            foreach (var key in allowedSymbols.Keys)
            {
                symbols.Add(key, 0);
            }
            foreach (var value in allowedSymbols.Values)
            {
                symbols.Add(value, 0);
            }

            // In a loop, we go through the text character by character.
            // If the character is equal to one of the valid characters, then increase the number in the dictionary by 1.
            // If after increasing the quantity for the current symbol it turns out that the quantity
            // closing more than the number of opening ones, then return false.
            foreach (var character in text)
            {
                int value = 0;
                if (symbols.TryGetValue(character, out value))
                {
                    symbols[character] = value + 1;
                    if (allowedSymbols.ContainsValue(character))
                    {
                        var key = allowedSymbols.FirstOrDefault(x => x.Value == character).Key;
                        if (symbols[character] > symbols[key])
                            return false;
                    }
                }
                else
                {
                    continue;
                }
            }

            // Run through valid characters, take keys, and use the key to find the closing character.
            // In the dictionary of symbols with values, get the number of closing and opening symbols.
            // If the number of characters is not equal, then return false, because the line is not balanced.
            // If the loop has ended, then return true.
            foreach (var openSymbol in allowedSymbols.Keys)
            {
                var closeSymbol = allowedSymbols[openSymbol];
                if (symbols[openSymbol] != symbols[closeSymbol])
                    return false;
            }
            return true;
        }
    }
}