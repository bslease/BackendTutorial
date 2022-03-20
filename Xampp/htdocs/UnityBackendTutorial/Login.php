<?php

require 'ConnectionSettings.php';

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

// normal statement and query (susceptible to injection)
// $sql = "SELECT password, id FROM users WHERE username = '" . $loginUser . "'";
// $result = $conn->query($sql);

// injection example
// if user submits ' OR id = '1 as their username, the normal method above would run:
// $sql = "SELECT password, id FROM users WHERE username = '' OR id = '1'";
// the user could log in without knowing the username (if they know the id and the password)
// similarly, sql injection could be used to drop entire tables, or query all sorts of info

// injection soluion: use a "prepared statement" (a feature of msqli (i = "improved"))
$sql = "SELECT password, id FROM users WHERE username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser); // "s" means type string
$statement->execute();
$result = $statement->get_result();

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