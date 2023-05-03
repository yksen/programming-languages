const http = require("http");
const fs = require("fs");
const socket = require("socket.io");

var html = fs.readFileSync("index.html");

var messages = ["Witaj"];
var users = [];
var id = 0;

var server = http.createServer(function (req, res) {
    res.end(html);
});

var userListInJSON = false;
var io = socket(server);

io.on("connection", function (socket) {

    socket.id = id++;
    socket.emit("id", socket.id);

    for (let message of messages)
        socket.emit("chat message", message);

    socket.on("chat message", function (message) {
        messages.push(message)
        io.emit("chat message", message);
    });

    socket.on("reset", function (message) {
        messages = [message]
        io.emit("reset", message);
    });

    socket.on("nick", function (data) {
        users.push({ id: socket.id, nick: data });
        if (userListInJSON)
            io.emit("user list", JSON.stringify(users));
        else {
            let nicks = "";
            for (let user of users)
                nicks += "<li>" + user.nick + "</li>";
            io.emit("user list", nicks);
        }
    });

    socket.on("disconnect", function () {
        users = users.filter(function (user) {
            return user.id != socket.id;
        });
        if (userListInJSON)
            io.emit("user list", JSON.stringify(users));
        else {
            let nicks = "";
            for (let user of users)
                nicks += "<li>" + user.nick + "</li>";
            io.emit("user list", nicks);
        }
    });

});

server.listen(8080);
