// socketio server
const express = require('express');
const app = express();
const http = require('http');
const server = http.createServer(app);
const { Server } = require("socket.io");
const io = new Server(server);

io.on('connection', async socket => {
    console.log(`Socket ${socket.id} connected`);
    socket.emit('/getData');
    socket.on('/sendStatus', async (response) => {
        console.log("Response received");
        console.log(`Response: ${response.id}, response-length: ${response.htmlData.length}\n\n\n`);
    });
    socket.on('disconnect', () => {
        console.log(`${socket.id} has disconnected`);
    });
    socket.on('error', (err) => {
        console.log("Error", err);
    })
})

server.listen(8089, () => {
    console.log('listening on *:8089');
});