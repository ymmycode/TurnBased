<?php

//might change later if using online database
$servername = "127.0.0.1";
$username = "root";  
$password = "";
$dbname = "gamedatabase";

//variables submitted by users/players
$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];
//$userID = $_POST["userID"];



// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully"."<br>";

$sql = "SELECT id FROM users WHERE username = '".$loginUser."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo $row["id"];
  }
} else {
  echo "Error";
}
$conn->close();
?>