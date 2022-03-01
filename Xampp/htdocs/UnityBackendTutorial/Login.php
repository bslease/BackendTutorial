<?php

require 'ConnectionSettings.php';

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password, id FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass) {
        echo $row["id"];
        //Get user's data here

        //Get player info

        //Get Inventory

        //Modify player data

        //Update inventory

    }

    else {
        echo "Wrong Credentials.";
    }
  }
} else {
  echo "Username does not exist.";
}
$conn->close();

?>