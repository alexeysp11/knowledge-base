namespace Studying.Leetcode.Arrays
{
    /// <summary>
    /// Description: https://leetcode.com/problems/concatenation-of-array/
    /// </summary>
    public class ArrayConcatenation : Studying.Leetcode.ILeetcodeProblem
    {
        public void Execute()
        {
            System.Console.WriteLine("Concatenation of Array\n".ToUpper()); 

            // Input: nums = [1,2,1]
            // Output: [1,2,1,1,2,1]
            // Explanation: The array ans is formed as follows:
            // - ans = [nums[0],nums[1],nums[2],nums[0],nums[1],nums[2]]
            // - ans = [1,2,1,1,2,1]
            int[] input1 = new int[] { 1, 2, 1 };
            int[] output1 = Concatenate(input1);
            Print(input1); 
            Print(output1); 

            // Input: nums = [1,3,2,1]
            // Output: [1,3,2,1,1,3,2,1]
            // Explanation: The array ans is formed as follows:
            // - ans = [nums[0],nums[1],nums[2],nums[3],nums[0],nums[1],nums[2],nums[3]]
            // - ans = [1,3,2,1,1,3,2,1]
            int[] input2 = new int[] { 1, 3, 2, 1 };
            int[] output2 = Concatenate(input2);
            Print(input2); 
            Print(output2); 
        }

        private int[] Concatenate(int[] nums)
        {
            // Constraints:
            // 1. n == nums.length
            // 2. 1 <= n <= 1000
            // 3. 1 <= nums[i] <= 1000

            int[] result = new int[0]; 
            try
            {
                if (nums.Length < 1 || nums.Length > 1000) 
                    throw new System.Exception("Violated constraint: 1 <= n <= 1000"); 

                result = new int[nums.Length * 2]; 
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] < 1 || nums[i] > 1000) 
                        throw new System.Exception("Violated constraint: 1 <= nums[i] <= 1000"); 
                    
                    result[i] = nums[i]; 
                    result[nums.Length + i] = nums[i]; 
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message); 
            }
            return result; 
        }

        private void Print(int[] array)
        {
            System.Console.Write("["); 
            for (int i = 0; i < array.Length; i++) System.Console.Write(array[i].ToString() + (i != array.Length - 1 ? "," : "")); 
            System.Console.Write("]\n"); 
        }
    }
}
