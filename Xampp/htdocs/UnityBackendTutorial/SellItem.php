<?php

require 'ConnectionSettings.php';

//variables submitted by user
$ID = $_POST["ID"];
$itemID = $_POST["itemID"];
$userID = $_POST["userID"];

//Check Connection
if($conn->connect_error){
    die("Connection failed: " . $conn->connect_error);
}

//First sql
$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'";

$result = $conn->query($sql);

if($result->num_rows > 0){

    //Store item price
    $itemPrice = $result->fetch_assoc()["price"];

    //Second Sql (delete item)
    $sql2 = "DELETE FROM usersitems WHERE ID = '" . $ID . "'";

    $result2 = $conn->query($sql2);
    if($result2){
        //If deleted successfully
        $sql3 = "UPDATE `users` SET `coins`= coins + '" . $itemPrice . "' WHERE `id` = '" . $userID . "'";
        $conn->query($sql3);
    }
    else {
        echo "error: could not delete item";
    }

} else {
    echo "0";
}
$conn->close();

?>