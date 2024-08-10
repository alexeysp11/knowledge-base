package rawsql 

import (
	"fmt"
	"strings"
	"database/sql"
	_ "github.com/mattn/go-sqlite3"
)

type NthHighestSalary struct {}

func (n *NthHighestSalary) Execute() {
	fmt.Println(strings.ToUpper("Find n-th highest salary\n"))

	// Declare variables 
	var i int = 0
	var nthInd int = 4 
	var salary float64
	
	// Open database 
	database, err := sql.Open("sqlite3", "../data/sqlite3/NthHighestSalary.db")
	if (err != nil) {
		fmt.Println(err)
	}

	// Execute select query 
	rows, err := database.Query("SELECT DISTINCT e.salary FROM Employee e ORDER BY e.salary DESC;")
	if (err != nil) {
		fmt.Println(err)
	}
	fmt.Println("salary")
	fmt.Println("----")
	for rows.Next() {
		i += 1 
		if i == nthInd {
			err := rows.Scan(&salary)
			if (err != nil) {
				fmt.Println(err)
			}
			fmt.Println(salary)
			break 
		}
	}
}