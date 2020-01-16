import socket,sys
from threading import Thread

globData = ""

def TCPServer():
    global globData
    address = "172.21.4.152"
    port = 9999
    # Create a TCP/IP socket
    socket.setdefaulttimeout(30000)
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # Then bind() is used to associate the socket with the server address. In this case, the address is localhost, referring to the current server, and the port number is 10000.

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
            print(sys.stderr, 'waiting for a connection')
            connection, client_address = sock.accept()
            print("waiting..")
            # accept() returns an open connection between the server and client, along with the address of the client. The connection is actually a different socket on another port (assigned by the kernel). Data is read from the connection with recv() and transmitted with sendall().

            try:

                print (sys.stderr, 'connection from', client_address)

                # Receive the data in small chunks and retransmit it
                while True:
                    data = connection.recv(10)
                    if '  ' not in data.decode():
                        print(data.decode())
                        globData = data.decode()

            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def Unity():
    global globData
    address = "172.21.4.152"
    port = 9998
    # Create a TCP/IP socket
    socket.setdefaulttimeout(30000)
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    # Then bind() is used to associate the socket with the server address. In this case, the address is localhost, referring to the current server, and the port number is 10000.

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
            print(sys.stderr, 'waiting for a connection')
            connection, client_address = sock.accept()
            print("waiting..")
            # accept() returns an open connection between the server and client, along with the address of the client. The connection is actually a different socket on another port (assigned by the kernel). Data is read from the connection with recv() and transmitted with sendall().

            try:

                print (sys.stderr, 'connection from', client_address)

                # Receive the data in small chunks and retransmit it
                while True:
                    print("sending:", globData)
                    connection.sendall(globData.encode())

            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

if __name__ == '__main__':
    p1 = Thread(target=TCPServer, args=())
    p2 = Thread(target=Unity, args=())

    p1.start()
    p2.start()

    p1.join()
    p2.join()
