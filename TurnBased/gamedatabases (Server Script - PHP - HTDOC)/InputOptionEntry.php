<?php

//might change later if using online database
$servername = "127.0.0.1";
$username = "root";  
$password = "";
$dbname = "gamedatabase";


//variables submitted by users/players
$userID = $_POST["userID"];


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
//echo "Connected successfully"."<br>";

$sql = "SELECT usersavedoption.userID FROM users, usersavedoption 
        WHERE users.id = '".$userID."' 
        AND users.id = usersavedoption.userID";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // tell the users that usesrname has been already taken
  // output data of each row
    echo "Entries Registered";
    
} else {

    //insert user id entry into 

    $sql1 = "INSERT INTO usersavedoption (userID)
    VALUES (".$userID.")";

        if ($conn->query($sql1) === TRUE) {
            echo "Entries Inserted";
        } else {
            echo "Error: " . $sql1 . "<br>" . $conn->error;
        }
}
$conn->close();
?>