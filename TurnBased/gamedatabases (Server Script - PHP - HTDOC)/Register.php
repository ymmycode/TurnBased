<?php

//might change later if using online database
$servername = "127.0.0.1";
$username = "root";  
$password = "";
$dbname = "gamedatabase";


//variables submitted by users/players
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];
$loginEmail = $_POST["loginEmail"];



// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully"."<br>";

$sql = "SELECT username FROM users WHERE username = '".$loginUser."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // tell the users that usesrname has been already taken
  // output data of each row
    echo "Username has been already taken.";
    
} else {
    echo "Creating Users . . . ";

    //insert name and password

    $sql1 = "INSERT INTO users (username, password, email)
    VALUES ('".$loginUser."', '".$loginPass."', '".$loginEmail."')";

        if ($conn->query($sql1) === TRUE) {
            echo "Account Created";
        } else {
            echo "Error: " . $sql1 . "<br>" . $conn->error;
        }

}
$conn->close();
?>