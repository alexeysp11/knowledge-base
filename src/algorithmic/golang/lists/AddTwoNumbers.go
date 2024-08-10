package lists 

import (
	"fmt"
	"strings"
	"bytes"
)

// Description: https://leetcode.com/problems/add-two-numbers/

type Node struct {
	val int
	next *Node
}

type LinkedList struct {
	firstNode *Node 
	lastNode *Node 
	length int 
}

func (l *LinkedList) Init(nodes []int) {
	if l.firstNode != nil {
		panic("The linked list is already initialized")
	}

	for i := 0; i < len(nodes); i++ {
		l.AddNode(nodes[i])
	}
}

func (l *LinkedList) AddNode(node int) {
	if (node < 0 || node > 9) {
		panic("Value of a node could not be less than 0 and bigger than 9")
	}
	if l.length > 100 {
		panic("Length of a linked list should not be bigger than 100")
	}

	nextNode := Node { val: node }
	if l.firstNode == nil {
		l.firstNode = &nextNode
	} else {
		l.lastNode.next = &nextNode
	}
	l.lastNode = &nextNode
	l.length += 1
}

func (l *LinkedList) AddLinkedList(list LinkedList) LinkedList {
	var output LinkedList = LinkedList {}
	var nodeBigger *Node  
	var nodeSmaller *Node  

	// get which list is bigger 
	if (l.length > list.length) {
		nodeBigger = l.firstNode 
		nodeSmaller = list.firstNode 
	} else {
		nodeBigger = list.firstNode 
		nodeSmaller = l.firstNode 
	}

	// assign new linked list
	remainder := 0 
	for nodeBigger != nil {
		val := remainder + nodeBigger.val
		if nodeSmaller != nil {
			val += nodeSmaller.val 
			nodeSmaller = nodeSmaller.next
		}
		nodeBigger = nodeBigger.next

		if val > 9 {
			remainder = 1 
			val -= 10 
		} else {
			remainder = 0
		}
		output.AddNode(val)
	}
	if remainder != 0 {
		output.AddNode(remainder)
	}

	return output
}

func (l *LinkedList) GetString() string {
	var buffer bytes.Buffer
	node := l.firstNode 
	buffer.WriteString("[")
	for node != nil {
		buffer.WriteString(fmt.Sprintf("%d", node.val))
		if node.next != nil {
			buffer.WriteString(",")
		}
		node = node.next
	}
	buffer.WriteString("]")
	return buffer.String()
}

type AddTwoNumbers struct {}

func (a *AddTwoNumbers) Solve(l1 LinkedList, l2 LinkedList) LinkedList {
	// Constraints:
	// 1. The number of nodes in each linked list is in the range [1, 100].
	// 2. 0 <= Node.val <= 9
	// 3. It is guaranteed that the list represents a number that does not have leading zeros.

	if l1.length < 1 || l1.length > 100 || l2.length < 1 || l2.length > 100 {
		panic("Violated constraint: The number of nodes in each linked list is in the range [1, 100]")
	}
	if ((l1.length != 1 && l1.lastNode.val == 0) || (l2.length != 1 && l2.lastNode.val == 0)) {
		panic("Violated constraint: It is guaranteed that the list represents a number that does not have leading zeros")
	}

	return l1.AddLinkedList(l2)
}

func (a *AddTwoNumbers) Execute() {
	fmt.Println(strings.ToUpper("Add Two Numbers\n"))
	
	// Example 1:
	// Input: l1 = [2,4,3], l2 = [5,6,4]
	// Output: [7,0,8]
	// Explanation: 342 + 465 = 807.
	l11 := LinkedList {}
	l12 := LinkedList {}
	l11.Init([]int { 2,4,3 })
	l12.Init([]int { 5,6,4 })
	output1 := a.Solve(l11, l12)
	fmt.Println("l1:", l11.GetString(), ", l2:", l12.GetString())
	fmt.Println("output:", output1.GetString())

	// Example 2:
	// Input: l1 = [0], l2 = [0]
	// Output: [0]
	l21 := LinkedList {}
	l22 := LinkedList {}
	l21.Init([]int { 0 })
	l22.Init([]int { 0 })
	output2 := a.Solve(l21, l22)
	fmt.Println("l1:", l21.GetString(), ", l2:", l22.GetString())
	fmt.Println("output:", output2.GetString())

	// Example 3:
	// Input: l1 = [9,9,9,9,9,9,9], l2 = [9,9,9,9]
	// Output: [8,9,9,9,0,0,0,1]
	l31 := LinkedList {}
	l32 := LinkedList {}
	l31.Init([]int { 9,9,9,9,9,9,9 })
	l32.Init([]int { 9,9,9,9 })
	output3 := a.Solve(l31, l32)
	fmt.Println("l1:", l31.GetString(), ", l2:", l32.GetString())
	fmt.Println("output:", output3.GetString())
}
