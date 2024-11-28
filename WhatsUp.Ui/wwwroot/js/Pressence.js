$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/pressenceHub").build();
    connection.start()
        .then(function () {
            console.log("Connected to the hub.");
        })
        .catch(function (error) {
            console.error(error);
        });

    connection.onclose(function () {
        console.log("Disconnected from the hub.");
    });

})

