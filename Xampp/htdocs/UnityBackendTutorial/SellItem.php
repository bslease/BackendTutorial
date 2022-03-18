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

// See Note below - as provided, users can spam the sell button for infinite coins
// so... let's first make sure the user actually has the item before we let them sell it
$sql0 = "SELECT * FROM usersitems WHERE ID = '" . $ID . "'";
$result = $conn->query($sql0);
if($result->num_rows == 0){
    echo "BEGONE FOUL GOLD DUPPER!";
    $conn->close();
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
        // NOTE: this seems to always return true even if the row has already been deleted
        echo $result2;
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