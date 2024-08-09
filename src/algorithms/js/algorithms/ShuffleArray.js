// Description: https://leetcode.com/problems/shuffle-the-array/

class ShuffleArray {
    execute() {
        var result = ''; 
        // Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
        // Return the array in the form [x1,y1,x2,y2,...,xn,yn].

        // Example 1:
        // Input: nums = [2,5,1,3,4,7], n = 3
        // Output: [2,3,5,4,1,7] 
        // Explanation: Since x1=2, x2=5, x3=1, y1=3, y2=4, y3=7 then the answer is [2,3,5,4,1,7].
        var input1 = [2,5,1,3,4,7]
        result += '{ { input: [' + input1 + '], output: [' + this.shuffle(input1, input1.length) + '] },'; 
        
        // Example 2:
        // Input: nums = [1,2,3,4,4,3,2,1], n = 4
        // Output: [1,4,2,3,3,2,4,1]
        var input2 = [1,2,3,4,4,3,2,1]
        result += '{ input: [' + input2 + '], output: [' + this.shuffle(input2, input2.length) + '] },'; 
        
        // Example 3:
        // Input: nums = [1,1,2,2], n = 2
        // Output: [1,2,1,2]
        var input3 = [1,1,2,2]
        result += '{ input: [' + input3 + '], output: [' + this.shuffle(input3, input3.length) + '] } }'; 

        return result; 
    }

    shuffle(inputArray, n) {
        var outputArray = []
        // numbers.forEach((n, i) => { 
        //     outputArray[2*i] = inputArray[i] 
        //     outputArray[2*i+1] = inputArray[i+n] 
        // });
        for (var i = 0; i < n; i++) {
            outputArray[2*i] = inputArray[i];
            outputArray[2*i+1] = inputArray[i+n];
        }
        return outputArray; 
    }

    static getLeetcodeProblemName() {
        return "Shuffle the Array".toUpperCase(); 
    }
}
