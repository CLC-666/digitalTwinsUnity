import socket,sys
from threading import Thread
from time import sleep
from datetime import datetime
from IPython import embed
import csv

manualData = ""
magFrontData = ""
stage = 1
data = []
timeStamps = []
full = {"Time" : timeStamps, "Data" : data}

fields = ['Order Number', 'Carrier ID', 'Actual Heat', 'Target Heat', 'Actual Time', 'Target Time', 'Full Station Time', 'World Time']


with open('heating.csv', "w") as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow(fields)

address = "172.21.4.150"
#address = "10.2.153.155"
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
            while True:
                dataIn = str(connection.recv(30))
                dataIn = dataIn.replace("b'", "")
                dataIn = dataIn.replace("'", "")
                # print(dataIn)
                if len(dataIn) == 30:
                    data.append(dataIn)
                    timeStamps.append(datetime.now())
                    print(connection.recv(1024))
                    try:
                        dataIn = dataIn.split(",")
                        dataIn.append(datetime.now())
                        with open('heating.csv', "a", newline = "") as csv_file:
                                writer = csv.writer(csv_file)
                                rows = [dataIn]
                                writer.writerows(rows)
                    except Exception as e:
                        print(e)
                    # order number, carrier ID, actual heat, target heat, actual time, target time, full station time



        except KeyboardInterrupt:
            # Clean up the connection
            connection.close()
            break

        except:
            ye = "man"

    except KeyboardInterrupt:
        connection.close()
        break

sock.close()
