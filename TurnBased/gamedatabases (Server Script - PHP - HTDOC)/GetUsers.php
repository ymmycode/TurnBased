<?php

//might change later if using online database
$servername = "127.0.0.1";
$username = "root";  
$password = "";
$dbname = "gamedatabase";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully"."<br>";

$sql = "SELECT username FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo $row["username"];
  }
} else {
  echo "0 results";
}
$conn->close();

?>