package algorithms

import (
	"fmt"
	"strings"
	"strconv"
)

// Description: https://leetcode.com/problems/partitioning-into-minimum-number-of-deci-binary-numbers/

type ParticioningDeciBanary struct {}

func (p *ParticioningDeciBanary) Solve(input string) int {
	var max int = -1
	for _, char := range input {
		i, err := strconv.Atoi(string(char))
		if err != nil {
			panic(err)
		}
		if i > max {
			max = i 
		}
	}
	return max 
}

func (p *ParticioningDeciBanary) Execute() {
	fmt.Println(strings.ToUpper("Partitioning into minimum number of deci-binary numbers\n"))

	// Example 1:
	// Input: n = "32"
	// Output: 3
	// Explanation: 10 + 11 + 11 = 32
	var input1 string = "32"
	output1 := p.Solve(input1)
	fmt.Println("input:", input1, "output:", output1)

	// Example 2:
	// Input: n = "82734"
	// Output: 8
	var input2 string = "82734"
	output2 := p.Solve(input2)
	fmt.Println("input:", input2, "output:", output2)

	// Example 3:
	// Input: n = "27346209830709182346"
	// Output: 9
	var input3 string = "27346209830709182346"
	output3 := p.Solve(input3)
	fmt.Println("input:", input3, "output:", output3)
}
