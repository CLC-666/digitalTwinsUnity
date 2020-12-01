import socket
import sys

address = socket.gethostbyname_ex(socket.gethostname())[-1][-1]
port = 9002

socket.setdefaulttimeout(30000)
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Then bind() is used to associate the socket with the server address. In this case, the address is localhost, referring to the current server, and the port number is 10000.

# Bind the socket to the port
server_address = (address, port)
print( 'starting up on %s port %s' % server_address)

sock.bind(server_address)
# Calling listen() puts the socket into server mode, and accept() waits for an incoming connection.

# Listen for incoming connections
sock.listen(1)

while 1:
    # Wait for a connection
    print ('waiting for a connection')


    connection, client_address = sock.accept()
    print(sys.stderr, 'connection from', client_address)
    # accept() returns an open connection between the server and client, along with the address of the client. The connection is actually a different socket on another port (assigned by the kernel). Data is read from the connection with recv() and transmitted with sendall().
    while 1:

        try:
            while 1:
                data = connection.recv(10)

                if data:
                    print(data)




        finally:
            # Clean up the connection
            connection.close()
