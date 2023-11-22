package main

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"log"
	"net/http"
	"strconv"
	"strings"

	_ "github.com/denisenkom/go-mssqldb"
)

var db *sql.DB

// User database model
type User struct {
	ID       int    `json:"id"`
	Name     string `json:"name"` // Değişiklik burada
	Email    string `json:"email"`
	Password string `json:"password"`
}

func InitializeDB() {
	// Connection details for MSSQL database
	server := "localhost"
	port := 1433
	user := "sa"
	password := "123"
	database := "exp"

	// Connect to MSSQL database
	connString := fmt.Sprintf("server=%s;user id=%s;password=%s;port=%d;database=%s",
		server, user, password, port, database)

	var err error
	db, err = sql.Open("sqlserver", connString)
	if err != nil {
		log.Fatal(err)
	}

	// Test the database connection
	err = db.PingContext(context.Background())
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println("Database connection successful.")
}

func main() {
	InitializeDB()
	defer db.Close()

	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprint(w, "WelIcome to the CRUD AP!")
	})
	http.HandleFunc("/users/", GetUser)
	http.HandleFunc("/users/create", CreateUser)
	http.HandleFunc("/users/update/", UpdateUser)
	http.HandleFunc("/users/delete/", DeleteUser)

	log.Fatal(http.ListenAndServe(":8080", nil))
}

func GetUsers(w http.ResponseWriter, r *http.Request) {
	rows, err := db.QueryContext(context.Background(), "SELECT * FROM Users")
	if err != nil {
		log.Fatal(err)
	}
	defer rows.Close()

	var users []User
	for rows.Next() {
		var user User
		err := rows.Scan(&user.ID, &user.Name, &user.Email, &user.Password) // Değişiklik burada
		if err != nil {
			log.Fatal(err)
		}
		users = append(users, user)
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(users)
}

func GetUser(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(strings.TrimPrefix(r.URL.Path, "/users/"))
	if err != nil {
		http.Error(w, "Invalid user ID", http.StatusBadRequest)
		return
	}
	var user User
	err = db.QueryRowContext(context.Background(), "SELECT * FROM Users WHERE id=@id", sql.Named("id", id)).Scan(&user.ID, &user.Name, &user.Email, &user.Password) // Değişiklik burada
	if err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			http.Error(w, "User not found", http.StatusNotFound)
			return
		}
		log.Fatal(err)
	}
	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(user)
}

func CreateUser(w http.ResponseWriter, r *http.Request) {
	var user User
	err := json.NewDecoder(r.Body).Decode(&user)
	if err != nil {
		http.Error(w, "Invalid request body", http.StatusBadRequest)
		return
	}

	user.ID = len(user.Name) + len(user.Email) + len(user.Password) // Değişiklik burada

	tsql := "INSERT INTO Users (name, email, password) VALUES (@name, @email, @password);"

	_, err = db.ExecContext(context.Background(), tsql,
		sql.Named("name", user.Name), // Değişiklik burada
		sql.Named("email", user.Email),
		sql.Named("password", user.Password))
	if err != nil {
		http.Error(w, "Error creating user: "+err.Error(), http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(user)
}

// Update a specific user
func UpdateUser(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(strings.TrimPrefix(r.URL.Path, "/users/update/"))
	if err != nil {
		log.Fatal(err)
	}

	var user User
	err = json.NewDecoder(r.Body).Decode(&user)
	if err != nil {
		log.Fatal(err)
	}

	tsql := "UPDATE Users SET name=@name, email=@email, password=@password WHERE id=@id"

	_, err = db.ExecContext(context.Background(), tsql,
		sql.Named("name", user.Name),
		sql.Named("email", user.Email),
		sql.Named("password", user.Password),
		sql.Named("id", id))
	if err != nil {
		log.Fatal(err)
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(user)
}

func DeleteUser(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(strings.TrimPrefix(r.URL.Path, "/users/delete/"))
	if err != nil {
		log.Fatal(err)
	}

	_, err = db.ExecContext(context.Background(), "DELETE FROM Users WHERE id=@id", sql.Named("id", id))
	if err != nil {
		log.Fatal(err)
	}

	fmt.Fprintf(w, "User with ID %v successfully deleted.", id)
}
