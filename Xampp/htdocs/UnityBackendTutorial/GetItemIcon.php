<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

//variables submitted by user
$itemID = $_POST["itemID"];
$urlHeader = $_POST["urlHeader"];

// $sql = "SELECT imageURL FROM items WHERE ID = '" . $itemID . "'";
// $path = "http://localhost/unitybackendtutorial/ItemsIcons/1.png";
// $path = "http://localhost/unitybackendtutorial/ItemsIcons/" . $itemID . ".png";
$path = $urlHeader . "ItemsIcons/" . $itemID . ".png";

// Get the image and convert into string
$image = file_get_contents($path);

echo $image;

// $result = $conn->query($sql);

$conn->close();

?>