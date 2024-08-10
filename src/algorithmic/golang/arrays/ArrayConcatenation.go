package arrays 

import (
	"fmt"
	"strings"
)

// Description: https://leetcode.com/problems/concatenation-of-array/ 

type ArrayConcatenation struct {}

func (a *ArrayConcatenation) Concatenate(input []int) []int {
	// Constraints:
	// 1. n == nums.length
	// 2. 1 <= n <= 1000
	// 3. 1 <= nums[i] <= 1000

	lenInput := len(input)
	if lenInput < 1 || lenInput > 1000 {
		panic("Violated constraint: 1 <= n <= 1000")
	}

	output := make([]int, lenInput*2)
	for i := 0; i < lenInput; i++ {
		if input[i] < 1 || input[i] > 1000 {
			panic("Violated constraint: 1 <= nums[i] <= 1000")
		}
		
		output[i] = input[i]
		output[i+lenInput] = input[i]
	}
	return output
}

func (a *ArrayConcatenation) Execute() {
	fmt.Println(strings.ToUpper("Concatenation of Array\n"))

	// Input: nums = [1,2,1]
	// Output: [1,2,1,1,2,1]
	// Explanation: The array ans is formed as follows:
	// - ans = [nums[0],nums[1],nums[2],nums[0],nums[1],nums[2]]
	// - ans = [1,2,1,1,2,1]
	input1 := []int {1,2,1}
	output1 := a.Concatenate(input1)
	fmt.Println("input:", input1, "output:", output1)

	// Input: nums = [1,3,2,1]
	// Output: [1,3,2,1,1,3,2,1]
	// Explanation: The array ans is formed as follows:
	// - ans = [nums[0],nums[1],nums[2],nums[3],nums[0],nums[1],nums[2],nums[3]]
	// - ans = [1,3,2,1,1,3,2,1]
	input2 := []int {1,3,2,1}
	output2 := a.Concatenate(input2)
	fmt.Println("input:", input2, "output:", output2)
}
