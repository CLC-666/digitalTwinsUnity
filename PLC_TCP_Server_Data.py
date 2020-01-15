import socket, sys, time

socket.setdefaulttimeout(30000)
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Then bind() is used to associate the socket with the server address. In this case, the address is localhost, referring to the current server, and the port number is 10000.
address = '10.3.121.133'
port = 9999
# Bind the socket to the port
server_address = (address, port)
print (sys.stderr, 'starting up on %s port %s' % server_address)

sock.bind(server_address)
# Calling listen() puts the socket into server mode, and accept() waits for an incoming connection.

# Listen for incoming connections
sock.listen(1)

while True:
    try:
        # Wait for a connection
        print (sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        # accept() returns an open connection between the server and client, along with the address of the client. The connection is actually a different socket on another port (assigned by the kernel). Data is read from the connection with recv() and transmitted with sendall().

        print (sys.stderr, 'connection from', client_address)

        # Receive the data in small chunks and retransmit it
        while True:
            try:
                connection.sendall("hello2".encode())
                time.sleep(1)
                # data = connection.recv(10)
                # if data:
                #     print(data)
                #     data = ''
                #
                #     # time.sleep(1)
                # else:
                #     print (sys.stderr, 'no more data from', client_address)
                #     dataFollowing = 0
                #     break
            except KeyboardInterrupt:
                break


    except KeyboardInterrupt:
        break
