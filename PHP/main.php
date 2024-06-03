<?php
$servername = "localhost";
$username = "username";
$password = "password";
$database = "products_db";

$conn = new mysqli($servername, $username, $password, $database);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

include 'product.php';
include 'user.php';

?>
