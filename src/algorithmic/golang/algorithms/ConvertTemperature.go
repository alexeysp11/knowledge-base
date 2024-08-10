package algorithms 

import "fmt"
import "strings"

// Description: https://leetcode.com/problems/convert-the-temperature/

type Temperature struct {
	Celsius float32
	Kelvin float32
	Farenheit float32
}

func (t *Temperature) Init(celsius float32) {
	// Constraints:
	// 0 <= celsius <= 1000

	if celsius < 0 || celsius > 1000 {
		panic("Violated constraint: 0 <= celsius <= 1000")
	}
	
	t.Celsius = celsius; 
	t.Kelvin = celsius + 273.15
	t.Farenheit = celsius * 1.80 + 32.00
}

type ConvertTemperature struct {
	Temperature Temperature
}

func (c *ConvertTemperature) Execute() {
	fmt.Println(strings.ToUpper("Convert the Temperature\n"))

	// Example 1:
	// Input: celsius = 36.50
	// Output: [309.65000,97.70000]
	// Explanation: Temperature at 36.50 Celsius converted in Kelvin is 309.65 and converted in Fahrenheit is 97.70.
	var c1 float32 = 36.50
	t1 := new(Temperature)
	t1.Init(c1)
	fmt.Println("celsius:", t1.Celsius, "kelvin:", t1.Kelvin, "Farenheit", t1.Farenheit)
	
	// Example 2:
	// Input: celsius = 122.11
	// Output: [395.26000,251.79800]
	// Explanation: Temperature at 122.11 Celsius converted in Kelvin is 395.26 and converted in Fahrenheit is 251.798.
	var c2 float32 = 122.11
	t2 := new(Temperature)
	t2.Init(c2)
	fmt.Println("celsius:", t2.Celsius, "kelvin:", t2.Kelvin, "Farenheit", t2.Farenheit)
}
