<?php
global $conn;

switch ($_SERVER['REQUEST_METHOD']) {
    case 'GET':
        $result = $conn->query("SELECT * FROM users");

        if ($result->num_rows > 0) {
            header('Content-Type: application/json');
            echo json_encode($result->fetch_all(MYSQLI_ASSOC));
        } else {
            echo "No users found.";
        }
        break;
    case 'POST':
        $data = json_decode(file_get_contents('php://input'), true);
        $name = $data['name'];
        $email = $data['email'];

        $sql = "INSERT INTO users (name, email) VALUES ('$name', '$email')";

        if ($conn->query($sql) === TRUE) {
            echo "New user created successfully.";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
        break;
    default:
        http_response_code(405);
        echo "Method not allowed.";
}
