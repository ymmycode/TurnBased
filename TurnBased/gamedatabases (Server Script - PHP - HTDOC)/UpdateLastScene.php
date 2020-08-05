<?php

//might change later if using online database
$servername = "127.0.0.1";
$username = "root";  
$password = "";
$dbname = "gamedatabase";


//variables submitted by users/players
$userID = $_POST["userID"];
$lastScene = $_POST["lastScene"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users WHERE id = '".$userID."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
    $sql1 = "UPDATE usersaveddata,users SET lastScene = '".$lastScene."' 
                WHERE users.id = '".$userID."' 
                AND usersaveddata.userID = users.id";

        if ($conn->query($sql1) === TRUE) {
            echo "Data Synced";
        } else {
            echo "Error: " . $sql1 . "<br>" . $conn->error;
        }
    
} else {
    echo "Error";
}
$conn->close();
?>