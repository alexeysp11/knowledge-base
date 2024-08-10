// Description: https://leetcode.com/problems/partitioning-into-minimum-number-of-deci-binary-numbers/

class ParticioningDeciBanary {
    execute() {
        // Example 1:
        // Input: n = "32"
        // Output: 3
        // Explanation: 10 + 11 + 11 = 32
        var input1 = '32'; 
        var result = '{ { input: ' + input1 + ', output: ' + this.solve(input1) + ' },'; 
        
        // Example 2:
        // Input: n = "82734"
        // Output: 8
        var input2 = '82734'; 
        result += '{ input: ' + input2 + ', output: ' + this.solve(input2) + ' },'; 

        // Example 3:
        // Input: n = "27346209830709182346"
        // Output: 9
        var input3 = '27346209830709182346'; 
        result += '{ input: ' + input3 + ', output: ' + this.solve(input3) + ' } }'; 
        
        return result; 
    }
    
    solve(aInputDigits) {
        // Constraints:
        // 1. 1 <= n.length <= 105
        // 2. n consists of only digits.
        // 3. n does not contain any leading zeros and represents a positive integer.

        var max = -1; 
        for (var i = 0; i < aInputDigits.length; i++) {
            var digit = parseInt(aInputDigits[i]); 
            if (digit > max) {
                max = digit; 
            }
        }
        return max; 
    }

    static getLeetcodeProblemName() {
        return "Partitioning into minimum number of deci-binary numbers".toUpperCase(); 
    }
}
