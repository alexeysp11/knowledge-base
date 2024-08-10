class DefangedIpAddress: 
    """
    Description: https://leetcode.com/problems/defanging-an-ip-address/
    """

    def defang(input_ip):
        chunks = []
        for c in input_ip: 
            if c == '.':
                chunks.append('[')
                chunks.append('.')
                chunks.append(']')
            else: 
                chunks.append(c)
        return ''.join(chunks)
    
    def execute(): 
        """
        Executes the algorithm 
        """

        print('Defanging an IP-address\n'.upper())

        # Example 1:
        # Input: address = "1.1.1.1"
        # Output: "1[.]1[.]1[.]1"
        input1 = "1.1.1.1"
        output1 = DefangedIpAddress.defang(input1)
        print(f'input: {input1}, output: {output1}')

        # Example 2:
        # Input: address = "255.100.50.0"
        # Output: "255[.]100[.]50[.]0"
        input2 = "255.100.50.0"
        output2 = DefangedIpAddress.defang(input2)
        print(f'input: {input2}, output: {output2}')
