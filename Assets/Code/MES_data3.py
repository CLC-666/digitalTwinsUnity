import socket


class unityTcpServer:
    data = ""
    def __init__(address, port):
        global data
        # Create a TCP/IP socket
        socket.setdefaulttimeout(30000)
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        server_address = (address, port)
        print (sys.stderr, 'starting up on %s port %s' % server_address)

        sock.bind(server_address)

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
                        sleep(0.1)
                        print("sending:", data)
                        connection.sendall(data.encode())


                except KeyboardInterrupt:
                    # Clean up the connection
                    connection.close()

                except:
                    ye = "man"

            except KeyboardInterrupt:
                connection.close()
                break

        sock.close()

        def sendData(self, data2):
            global data
            data = data2



def main():
    magFront = unityTcpServer()



if __name__ == '__main__':
    main()
