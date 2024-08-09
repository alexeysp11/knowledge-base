package rawsql; 

import (
	"fmt"
	"strings"
	"database/sql"
	_ "github.com/mattn/go-sqlite3"
)

type InvalidTweets struct {}

func (t *InvalidTweets) Execute() {
	fmt.Println(strings.ToUpper("Invalid Tweets\n"))

	// Declare variables 
	var id int 
	var content string 

	// Open database 
	database, err := sql.Open("sqlite3", "../data/sqlite3/InvalidTweets.db")
	if (err != nil) {
		fmt.Println(err)
	}

	// Read file that initializes the database 
	// file, err := os.Open("../sql/sqlite3/Config_InvalidTweets.sql")
    // if err != nil {
    //     log.Fatal(err)
    // }
    // defer func() {
    //     if err = file.Close(); err != nil {
    //         log.Fatal(err)
    //     }
    // }()

	// // Get the content of the file 
	// var buffer bytes.Buffer
	// scanner := bufio.NewScanner(file)
    // for scanner.Scan() { 
	// 	buffer.WriteString(scanner.Text() + "\n")
    // }

	// Ininitialize the database 
	// statement, err := database.Prepare(buffer.String())
	// if (err != nil) {
	// 	fmt.Println(err)
	// }
	// statement.Exec()

	// statement, err := database.Prepare("insert into tweets (content) values ('some new content');")
	// if (err != nil) {
	// 	fmt.Println(err)
	// }
	// statement.Exec()

	// Execute select query 
	rows, err := database.Query("SELECT t.tweet_id, t.content FROM Tweets t WHERE length(t.content) > 15;")
	if (err != nil) {
		fmt.Println(err)
	}
	fmt.Println("id | content")
	fmt.Println("---+--------")
	for rows.Next() {
		err := rows.Scan(&id, &content)
		if (err != nil) {
			fmt.Println(err)
		}
		fmt.Println(id, " | ", content)
	}
}
