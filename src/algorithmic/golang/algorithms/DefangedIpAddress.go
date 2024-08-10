package algorithms 

import (
	"fmt"
	"bytes"
	"strings"
)

// Description: https://leetcode.com/problems/defanging-an-ip-address/

type DefangedIpAddress struct {}

func (d *DefangedIpAddress) Defang(input string) string {
	var buffer bytes.Buffer
	for _, char := range input {
		if char == '.' {
			buffer.WriteString("[.]")
		} else {
			buffer.WriteString(string(char))
		}
	}
	return buffer.String()
}

func (d *DefangedIpAddress) Execute() {
	fmt.Println(strings.ToUpper("Defanging an IP-address\n"))

	// Example 1:
	// Input: address = "1.1.1.1"
	// Output: "1[.]1[.]1[.]1"
	var input1 string = "1.1.1.1"
	output1 := d.Defang(input1)
	fmt.Println("input:", input1, "output:", output1)

	// Example 2:
	// Input: address = "255.100.50.0"
	// Output: "255[.]100[.]50[.]0"
	var input2 string = "255.100.50.0"
	output2 := d.Defang(input2)
	fmt.Println("input:", input2, "output:", output2)
}
