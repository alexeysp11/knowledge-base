class ParticioningDeciBanary: 
    """
    Description: https://leetcode.com/problems/partitioning-into-minimum-number-of-deci-binary-numbers/
    """

    def solve(input_num): 
        max_num = -1
        for c in input_num: 
            tmp_num = int(c)
            if tmp_num > max_num: 
                max_num = tmp_num
        return max_num
    
    def execute(): 
        """
        Executes the algorithm
        """

        print('Partitioning into minimum number of deci-binary numbers\n'.upper())
        
        # Example 1:
        # Input: n = "32"
        # Output: 3
        # Explanation: 10 + 11 + 11 = 32
        input1 = "32"
        output1 = ParticioningDeciBanary.solve(input1)
        print(f'input: {input1}, output: {output1}')

        # Example 2:
        # Input: n = "82734"
        # Output: 8
        input2 = "82734"
        output2 = ParticioningDeciBanary.solve(input2)
        print(f'input: {input2}, output: {output2}')

        # Example 3:
        # Input: n = "27346209830709182346"
        # Output: 9
        input3 = "27346209830709182346"
        output3 = ParticioningDeciBanary.solve(input3)
        print(f'input: {input3}, output: {output3}')
