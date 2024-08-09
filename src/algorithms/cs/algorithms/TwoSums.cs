using System.Linq;
using System.Collections.Generic;

namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Description: https://leetcode.com/problems/two-sum/
    /// </summary>
    public class TwoSums : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // Example 1:
            // Input: nums = [2,7,11,15], target = 9
            // Output: [0,1]
            // Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
            var nums01 = new int[] {2,7,11,15};
            var target01 = 9;
            var result01 = TwoSum(nums01, target01);
            PrintInput(nums01, target01);
            PrintResult(result01);

            // Example 2:
            // Input: nums = [3,2,4], target = 6
            // Output: [1,2]
            var nums02 = new int[] {3,2,4};
            var target02 = 6;
            var result02 = TwoSum(nums02, target02);
            PrintInput(nums02, target02);
            PrintResult(result02);

            // Example 3:
            // Input: nums = [3,3], target = 6
            // Output: [0,1]
            var nums03 = new int[] {3,3};
            var target03 = 6;
            var result03 = TwoSum(nums03, target03);
            PrintInput(nums03, target03);
            PrintResult(result03);
        }

        private int[] TwoSum(int[] nums, int target) 
        {
            var result = new List<int>();
            var potentials = new List<int>();

            // Loop through the entire array.
            // If the element is larger than expected, then skip the element.
            // If the element is smaller than expected, then save the element to the list of potential elements
            // and run the nested array, skipping the index of the current element.
            // If adding the elements of the first and second loop results in the desired value,
            // then we add these elements to the result and break the loop (otherwise we return an empty array).
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > target)
                    continue;
                potentials.Add(i);
                foreach (var potential in potentials)
                {
                    if (i == potential) continue;
                    if (target == nums[i] + nums[potential])
                    {
                        result.Add(i);
                        result.Add(potential);
                        goto ReturnResult;
                    }
                }
            }
            
            ReturnResult:
                return result.OrderBy(x => x).ToArray();
        }

        private void PrintInput(int[] nums, int target)
        {
            System.Console.Write("Input: ");
            System.Console.Write("nums = [");
            PrintArray(nums);
            System.Console.Write("], target = ");
            System.Console.Write(target);
            System.Console.WriteLine("");
        }

        private void PrintResult(int[] nums)
        {
            System.Console.Write("Result: ");
            PrintArray(nums);
            System.Console.WriteLine("");
        }

        private void PrintArray(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                System.Console.Write(nums[i] + (i != nums.Length - 1 ? " " : string.Empty));
            }
        }
    }
}