package algorithms 

import (
	"fmt"
	"strings"
)

// Description: https://leetcode.com/problems/shuffle-the-array/

type ShuffleArray struct {}

func (s *ShuffleArray) Shuffle(input []int, n int) []int {
	// Constraints:
	// 1. 1 <= n <= 500
	// 2. nums.length == 2n
	// 3. 1 <= nums[i] <= 10^3
	if n < 1 || n > 500 {
		panic("Violated constraint: 1 <= n <= 500")
	}
	if len(input) != 2*n {
		panic("Violated constraint: nums.length == 2n")
	}

	output := make([]int, len(input))
	for i := 0; i < n; i++ {
		if input[i] < 1 || input[i] > 1000 || input[i+n] < 1 || input[i+n] > 1000 {
			panic("Violated constraint: 1 <= nums[i] <= 10^3")
		}
		output[2*i] = input[i]
		output[2*i+1] = input[i+n]
	}
	return output
}

func (s *ShuffleArray) Execute() {
	fmt.Println(strings.ToUpper("Shuffle the Array\n"))

	// Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
	// Return the array in the form [x1,y1,x2,y2,...,xn,yn].

	// Example 1:
	// Input: nums = [2,5,1,3,4,7], n = 3
	// Output: [2,3,5,4,1,7] 
	// Explanation: Since x1=2, x2=5, x3=1, y1=3, y2=4, y3=7 then the answer is [2,3,5,4,1,7].
	input1 := []int {2,5,1,3,4,7}
	output1 := s.Shuffle(input1, len(input1)/2)
	fmt.Println("input:", input1, "output:", output1)

	// Example 2:
	// Input: nums = [1,2,3,4,4,3,2,1], n = 4
	// Output: [1,4,2,3,3,2,4,1]
	input2 := []int {1,2,3,4,4,3,2,1}
	output2 := s.Shuffle(input2, len(input2)/2)
	fmt.Println("input:", input2, "output:", output2)

	// Example 3:
	// Input: nums = [1,1,2,2], n = 2
	// Output: [1,2,1,2]
	input3 := []int {1,1,2,2}
	output3 := s.Shuffle(input3, len(input3)/2)
	fmt.Println("input:", input3, "output:", output3)
}
