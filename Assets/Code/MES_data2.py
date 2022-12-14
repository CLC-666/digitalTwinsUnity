import socket,sys
from threading import Thread
from time import sleep

manualData = ""
magFrontData = ""
stage = 1


def magFrontStation():
    global magFrontData
    global stage
    # address = "172.21.4.151"
    address = "192.168.1.86"
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
                while True: #station, firstinduc, secondInduc, carrierReleased, thirdInduc, carrierid
                    data = connection.recv(12)
                    if '  ' not in data.decode():
                        magFrontData = data.decode()
                        print(magFrontData)
                        # var = int(manualData.split(",")[4])
                        # if var == 1:
                        #     stage = 3

            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def UnityMagFront():
    global manualData
    global magFrontData
    global stage
    address = "172.21.4.151"
    # address = "10.2.194.147"
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
                    sleep(0.1)
                    print("sending:", magFrontData)
                    connection.sendall(magFrontData.encode())


            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def manualStation():
    global manualData
    global stage
    address = "172.21.4.151"
    #address = "10.2.194.147"
    port = 9997
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
                while True: #station, firstinduc, secondInduc, carrierReleased, thirdInduc, carrierid
                    data = connection.recv(12)
                    if '  ' not in data.decode():
                        # print(data.decode())
                        var = 0
                        manualData = data.decode()
                        # var = int(manualData.split(",")[4])
                        # if var == 1:
                        #     stage = 3

            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def UnityManual():
    global manualData
    global magFrontData
    global stage
    address = "172.21.4.151"
    # address = "10.2.194.147"
    port = 9996
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
                    sleep(0.1)
                    print("sending:", manualData)
                    connection.sendall(manualData.encode())


            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def cameraStation():
    global cameraData
    global stage
    address = "172.21.4.151"
    #address = "10.2.194.147"
    port = 9997
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
                while True: #station, firstinduc, secondInduc, carrierReleased, thirdInduc, carrierid
                    data = connection.recv(12)
                    if '  ' not in data.decode():
                        # print(data.decode())
                        var = 0
                        cameraData = data.decode()
                        # var = int(manualData.split(",")[4])
                        # if var == 1:
                        #     stage = 3

            except KeyboardInterrupt:
                # Clean up the connection
                connection.close()

            except:
                ye = "man"

        except KeyboardInterrupt:
            connection.close()
            break

    sock.close()

def UnityManual():
    global manualData
    global magFrontData
    global stage
    address = "172.21.4.151"
    # address = "10.2.194.147"
    port = 9996
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
                    sleep(0.1)
                    print("sending:", manualData)
                    connection.sendall(manualData.encode())


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
    p1 = Thread(target=manualStation, args=())
    p2 = Thread(target=UnityManual, args=())
    p3 = Thread(target=magFrontStation, args=())
    p4 = Thread(target=UnityMagFront, args=())

    p1.start()
    p2.start()
    p3.start()
    p4.start()

    p1.join()
    p2.join()
    p3.join()
    p4.join()
