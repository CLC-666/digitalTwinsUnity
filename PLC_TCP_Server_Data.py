import socket, sys, time

socket.setdefaulttimeout(30000)
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Then bind() is used to associate the socket with the server address. In this case, the address is localhost, referring to the current server, and the port number is 10000.
address = '10.3.201.147'
port = 9999
# Bind the socket to the port
server_address = (address, port)
print (sys.stderr, 'starting up on %s port %s' % server_address)

sock.bind(server_address)
# Calling listen() puts the socket into server mode, and accept() waits for an incoming connection.
sending = ["0","1","2"]
counter = 0
c = 0
# Listen for incoming connections
sock.listen(1)

done = False

while True:
    try:
        # Wait for a connection
        print (sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        # accept() returns an open connection between the server and client, along with the address of the client. The connection is actually a different socket on another port (assigned by the kernel). Data is read from the connection with recv() and transmitted with sendall().

        print (sys.stderr, 'connection from', client_address)
        old = round(time.time())
        oldList = list(str(old))
        mod = oldList[(len(oldList)-1):]
        mod = int(mod[0])
        print("mod", mod)
        # Receive the data in small chunks and retransmit it
        while True:
            try:
                var = round(time.time())
                if var % mod == 0 and done == True:
                    print("sending", sending[c])
                    connection.sendall(sending[c].encode())
                    c += 1
                if var % mod != 0:
                    done = False
                    connection.sendall("".encode())
            except KeyboardInterrupt:
                break


    except KeyboardInterrupt:
        break
