# WebSocket-Test
## Summary
The Solution includes two Projects:
### Server App:
    1. The Server Accept Websocket connetion in ws://localhost:5000/messages and response with Client ID and a wellcome messsage within Websocket

    2. The Server accepts Http Post to /ping and respond with sending websocket message "Pong" to the client.

    3. The Server accepts http Post request to /work/started and response with workstarted and ID withing websocket and after a few seconds send another message for workFinished and ID;

    4. the Client is supposed to include ClientId in the header of post requests. So that server identify the correct websocket session.
    
### Client App:
    The Client sends the connection , Ping, and Work start requests and shows all the messages receiving withing websocket. 

## Prerequestiges:
1. .Net 6 SDK
2. Visual Studio 2022 Or VS Code

## Run the Solution:

### Using Viusal Studio
1. Open The solution in Visual Studio 2022.
2. The solution is configured to run both projects together.
3. run the projects
4. In the Client app write the server URL and Press connect

### Using VS Code:
1. open the WebSocket-Test folder
2. Goto Run and Debug section
3. In the Configuration combo Run ServerApp and then Run ClientApp.


