"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var loggedUser = document.getElementById("userInput").value;
    if(loggedUser == user){
        var encodedMsg = "You said: " + msg;
    } else {
        var encodedMsg = user + " says: " + msg;

    }
    var li = document.createElement("li");
    if(loggedUser == user){
        
        li.className = "list-group-item mt-2 text-right";
    } else {
        li.className = "list-group-item mt-2";

    }
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if(!message) {
        return;
    }
    var userId = parseInt(document.getElementById("userId").value);
    
    connection.invoke("SendMessage", user, message, userId).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageInput").value = "";
    event.preventDefault();
});