<?php
global $conn;
switch ($_SERVER['REQUEST_METHOD']) {
    case 'POST':
        $data = json_decode(file_get_contents('php://input'), true);
        $name = $data['name'];
        $price = $data['price'];

        $sql = "INSERT INTO products (name, price) VALUES ('$name', $price)";

        if ($conn->query($sql) === TRUE) {
            echo "New product created successfully.";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
        break;
    case 'GET':
        $result = $conn->query("SELECT * FROM products");

        if ($result->num_rows > 0) {
            header('Content-Type: application/json');
            echo json_encode($result->fetch_all(MYSQLI_ASSOC));
        } else {
            echo "No products found.";
        }
        break;
    case 'PUT':
        parse_str(file_get_contents("php://input"), $_PUT);
        $id = $_PUT["id"];
        $name = $_PUT["name"];
        $price = $_PUT["price"];

        $sql = "UPDATE products SET name='$name', price=$price WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "Product updated successfully.";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
        break;
    case 'DELETE':
        parse_str(file_get_contents("php://input"), $_DELETE);
        $id = $_DELETE["id"];

        $sql = "DELETE FROM products WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "Product deleted successfully.";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
        break;
    default:
        http_response_code(405);
        echo "Method not allowed.";
}
