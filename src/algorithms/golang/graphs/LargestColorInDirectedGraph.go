package graphs

import (
	"fmt"
	"strings"
)

// Description: https://leetcode.com/problems/largest-color-value-in-a-directed-graph/

type LargestColorInDirectedGraph struct {}

func (l *LargestColorInDirectedGraph) Execute() {
	fmt.Println(strings.ToUpper("Largest Color Value in a Directed Graph\n"))
	
	// Input: colors = "abaca", edges = [[0,1],[0,2],[2,3],[3,4]]
	// Output: 3
	// Explanation: The path 0 -> 2 -> 3 -> 4 contains 3 nodes that are colored "a" (red in the above image).
}
