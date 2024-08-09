class ShuffleArray: 
    """
    Description: https://leetcode.com/problems/shuffle-the-array/
    """

    def shuffle(input_array, n): 
        # Constraints:
        # 1. 1 <= n <= 500
        # 2. nums.length == 2n
        # 3. 1 <= nums[i] <= 10^3

        if n < 1 or n > 500: 
            raise Exception('Violated constraint: 1 <= n <= 500')
        if len(input_array) != 2*n: 
            raise Exception('Violated constraint: nums.length == 2n')

        output_array = []
        for i in range(int(n)): 
            if input_array[i] < 1 or input_array[i] > 1000: 
                raise Exception('Violated constraint: 1 <= nums[i] <= 10^3') 
            output_array.append(input_array[int(i)])
            output_array.append(input_array[int(i+n)])
        return output_array

    def execute(): 
        """
        Executes the algorithm
        """

        print('Shuffle the Array\n'.upper())

        # Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
        # Return the array in the form [x1,y1,x2,y2,...,xn,yn].

        # Example 1:
        # Input: nums = [2,5,1,3,4,7], n = 3
        # Output: [2,3,5,4,1,7] 
        # Explanation: Since x1=2, x2=5, x3=1, y1=3, y2=4, y3=7 then the answer is [2,3,5,4,1,7].
        input1 = [2,5,1,3,4,7]
        output1 = ShuffleArray.shuffle(input1, len(input1)/2)
        print(f'input: {input1}, output: {output1}')

        # Example 2:
        # Input: nums = [1,2,3,4,4,3,2,1], n = 4
        # Output: [1,4,2,3,3,2,4,1]
        input2 = [1,2,3,4,4,3,2,1]
        output2 = ShuffleArray.shuffle(input2, len(input2)/2)
        print(f'input: {input2}, output: {output2}')

        # Example 3:
        # Input: nums = [1,1,2,2], n = 2
        # Output: [1,2,1,2]
        input3 = [1,1,2,2] 
        output3 = ShuffleArray.shuffle(input3, len(input3)/2)
        print(f'input: {input3}, output: {output3}')

