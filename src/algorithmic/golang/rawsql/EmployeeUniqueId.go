package rawsql 

import (
	"fmt"
	"strings"
	"database/sql"
	_ "github.com/mattn/go-sqlite3"
)

type Employee struct {
	id int 
	name string
}

type EmployeeUniqueId struct {}

func (t *EmployeeUniqueId) Execute() {
	fmt.Println(strings.ToUpper("Replace employee id with the unique identifier\n"))

	// Open database 
	database, err := sql.Open("sqlite3", "../data/sqlite3/EmployeeUniqueId.db")
	if (err != nil) {
		fmt.Println(err)
	}

	// Execute select query 
	rows, err := database.Query(`
		SELECT coalesce(euni.unique_id, 0), coalesce(e.name, '')
		FROM Employees e 
		LEFT JOIN EmployeeUNI euni ON e.id = euni.id 
	`)
	if (err != nil) {
		fmt.Println(err)
	}
	fmt.Println("id | name")
	fmt.Println("---+--------")
	for rows.Next() {
		var employee Employee
		err := rows.Scan(&employee.id, &employee.name)
		if (err != nil) {
			fmt.Println(err)
		}
		fmt.Println(employee.id, " | ", employee.name)
	}
}
