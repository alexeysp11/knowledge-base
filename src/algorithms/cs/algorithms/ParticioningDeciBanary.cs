namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Description: https://leetcode.com/problems/partitioning-into-minimum-number-of-deci-binary-numbers/
    /// </summary>
    public class ParticioningDeciBanary : Studying.Leetcode.ILeetcodeProblem
    {
        public void Execute()
        {
            System.Console.WriteLine("Partitioning into minimum number of deci-binary numbers\n".ToUpper()); 
            // Example 1:
            // Input: n = "32"
            // Output: 3
            // Explanation: 10 + 11 + 11 = 32
            string input1 = "32"; 
            int output1 = Solve(input1); 
            System.Console.WriteLine("input: " + input1); 
            System.Console.WriteLine("output: " + output1); 

            // Example 2:
            // Input: n = "82734"
            // Output: 8
            string input2 = "82734"; 
            int output2 = Solve(input2); 
            System.Console.WriteLine("input: " + input2); 
            System.Console.WriteLine("output: " + output2); 

            // Example 3:
            // Input: n = "27346209830709182346"
            // Output: 9
            string input3 = "27346209830709182346"; 
            int output3 = Solve(input3); 
            System.Console.WriteLine("input: " + input3); 
            System.Console.WriteLine("output: " + output3); 
        }

        private int Solve(string input)
        {
            // Constraints:
            // 1. 1 <= n.length <= 105
            // 2. n consists of only digits.
            // 3. n does not contain any leading zeros and represents a positive integer.

            int maxNum = -1; 
            try
            {
                if (string.IsNullOrEmpty(input) || input.Length < 1 || input.Length > 105) 
                    throw new System.Exception("Violated constraint: 1 <= n.length <= 105"); 
                if (input[0] == '0') 
                    throw new System.Exception("Violated constraint: input should not contain any leading zeros"); 

                foreach (char c in input)
                {
                    if (!char.IsDigit(c)) 
                        throw new System.Exception("Violated constraint: value should consists of only digits"); 
                    
                    int tmp = int.Parse(c.ToString());
                    if (tmp > maxNum) maxNum = tmp;
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Exception: " + ex.Message); 
                return -1; 
            }
            return maxNum; 
        }
    }
}
