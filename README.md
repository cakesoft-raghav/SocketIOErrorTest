# SocketIOErrorTest
Reproduces error described in doghappy/socket.io-client-csharp#234

## Steps to reproduce error

### Server
- Run `npm i` to install dependencies
- Run `npm run start` to run server

### Client
- Import SocketIOClientApp into Visual Studio
- Build and Run

After receiving a few emits, the server will error out with the error message - `Error: Invalid Payload`

## Steps to get details on the error
Involves making changes in the socketio-parser file in node-modules

### Edit socketio-parser file
 - Go to file `index.js` in `node_modules/socketio-parser/dist` in socketIOServer
 - Find the function `decodeString`, add `console.log(str)` to it to get the incoming json string
 - Find the function `tryParse`, replace it's content with `return JSON.parse(str)` to see the actual JSON parsing error

When the JSON.parse errors out, you will see the exact position of the error in json string and also the raw json string in the terminal
