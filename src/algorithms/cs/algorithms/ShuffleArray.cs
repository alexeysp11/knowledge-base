namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Description: https://leetcode.com/problems/shuffle-the-array/
    /// </summary>
    public class ShuffleArray : Studying.Leetcode.ILeetcodeProblem
    {
        public void Execute()
        {
            System.Console.WriteLine("Shuffle the Array\n".ToUpper()); 

            // Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
            // Return the array in the form [x1,y1,x2,y2,...,xn,yn].

            // Example 1:
            // Input: nums = [2,5,1,3,4,7], n = 3
            // Output: [2,3,5,4,1,7] 
            // Explanation: Since x1=2, x2=5, x3=1, y1=3, y2=4, y3=7 then the answer is [2,3,5,4,1,7].
            int[] input1 = new int[] { 2,5,1,3,4,7 };
            int[] output1 = Shuffle(input1, input1.Length/2); 
            Print(input1); 
            Print(output1); 

            // Example 2:
            // Input: nums = [1,2,3,4,4,3,2,1], n = 4
            // Output: [1,4,2,3,3,2,4,1]
            int[] input2 = new int[] { 1,2,3,4,4,3,2,1 }; 
            int[] output2 = Shuffle(input2, input2.Length/2); 
            Print(input2); 
            Print(output2); 

            // Example 3:
            // Input: nums = [1,1,2,2], n = 2
            // Output: [1,2,1,2]
            int[] input3 = new int[] { 1,1,2,2 }; 
            int[] output3 = Shuffle(input3, input3.Length/2); 
            Print(input3); 
            Print(output3); 
        }

        private int[] Shuffle(int[] input, int n)
        {
            // Constraints:
            // 1. 1 <= n <= 500
            // 2. nums.length == 2n
            // 3. 1 <= nums[i] <= 10^3
            
            if (n < 1 || n > 500) 
                throw new System.Exception("Violated constraint: 1 <= n <= 500"); 
            if (input.Length != 2*n) 
                throw new System.Exception("Violated constraint: nums.length == 2n"); 

            int[] output = new int[input.Length]; 
            for (int i = 0; i < n; i++)
            {
                int lower = 1, upper = 1000; 
                if (input[i] < lower || input[i] > upper || input[i + n] < lower || input[i + n] > upper) 
                    throw new System.Exception("Violated constraint: 1 <= nums[i] <= 10^3"); 
                
                output[2*i] = input[i]; 
                output[2*i + 1] = input[i + n]; 
            }
            return output; 
        }

        private void Print(int[] array)
        {
            System.Console.Write("["); 
            for (int i = 0; i < array.Length; i++) System.Console.Write(array[i] + (i != array.Length - 1 ? "," : "")); 
            System.Console.Write("]\n"); 
        }
    }
}
