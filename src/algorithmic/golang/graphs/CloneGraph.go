package graphs 

import (
	"fmt"
	"strings"
	"bytes"
)

// Description: https://leetcode.com/problems/clone-graph/description/ 

type Node struct {
	val int
	neighbors []*Node 
}

func (n *Node) GetNeighborsString() string {
	var buffer bytes.Buffer
	buffer.WriteString("[")
	for i := 0; i < len(n.neighbors); i++ {
		buffer.WriteString(fmt.Sprintf("%d", n.neighbors[i].val))
		if i < len(n.neighbors) - 1 {
			buffer.WriteString(",")
		}
	}
	buffer.WriteString("]")
	return buffer.String()
}

type CloneGraph struct {}

func (c *CloneGraph) PrintArray(msg string, array []Node) {
	fmt.Print(msg, "[")
	for i := 0; i < len(array); i++ {
		fmt.Print(array[i].GetNeighborsString())
		if i < len(array) - 1 {
			fmt.Print(",")
		}
	}
	fmt.Print("]\n")
}

func (c *CloneGraph) Clone(input []Node) []Node {
	// Constraints:
	// 1. The number of nodes in the graph is in the range [0, 99].
	// 2. 1 <= Node.val <= 100
	// 3. Node.val is unique for each node.
	// 4. There are no repeated edges and no self-loops in the graph.
	// 5. The Graph is connected and all nodes can be visited starting from the given node.

	if len(input) > 99 {
		panic("Violated constraint: The number of nodes in the graph is in the range [0, 99]")
	}

	output := make([]Node, len(input)) 
	for i := 0; i < len(input); i++ {
		if input[i].val < 1 || input[i].val > 100 {
			panic("Violated constraint: 1 <= Node.val <= 100")
		}
		// tmpNode := input[i]
		output[i] = input[i]
	}
	return input
}

func (c *CloneGraph) Execute() {
	fmt.Println(strings.ToUpper("Clone Graph\n"))

	// Input: adjList = [[2,4],[1,3],[2,4],[1,3]]
	// Output: [[2,4],[1,3],[2,4],[1,3]]
	input1 := []Node {
		Node { val: 1, neighbors: []*Node { &Node{val: 2}, &Node{val: 4} } },
		Node { val: 2, neighbors: []*Node { &Node{val: 1}, &Node{val: 3} } },
		Node { val: 3, neighbors: []*Node { &Node{val: 2}, &Node{val: 4} } },
		Node { val: 4, neighbors: []*Node { &Node{val: 1}, &Node{val: 3} } },
	}
	output1 := c.Clone(input1)
	c.PrintArray("input:", input1)
	c.PrintArray("output:", output1)

	// Input: adjList = [[]]
	// Output: [[]]
	input2 := []Node { Node { val: 1 } }
	output2 := c.Clone(input2)
	c.PrintArray("input:", input2)
	c.PrintArray("output:", output2)

	// Input: adjList = []
	// Output: []
	input3 := make([]Node, 0)
	output3 := c.Clone(input3)
	c.PrintArray("input:", input3)
	c.PrintArray("output:", output3)
}
