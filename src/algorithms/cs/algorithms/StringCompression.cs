using System.Linq; 

namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    public class StringCompression : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// Given a string of lower-case letters, it's necessasry to return a compressed string. 
        /// For example: input - 'aaabbcccdde', output - 'a3b2c3d2e'
        /// </summary>
        public void Execute()
        {
            string[] examples = { "aaabbcccdde", "tooooseewqqqooo" }; 
            foreach (string example in examples) 
            {
                string output = CompressString(example);
                System.Console.WriteLine($"input: {example}, output: {output}"); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string CompressString(string input)
        {
            if (input.Count(c => char.IsLower(c)) != input.Length) 
                throw new System.Exception($"Input string '{input}' should only consist of lower-case letters"); 

            string result = string.Empty; 
            int occurances = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (!result.EndsWith(input[i])) 
                {
                    result += (occurances > 1 ? occurances.ToString() : "") + input[i]; 
                    occurances = 1; 
                    continue; 
                }
                occurances += 1; 
                if (i == input.Length - 1) result += occurances.ToString(); 
            }
            return result; 
        }
    }
}