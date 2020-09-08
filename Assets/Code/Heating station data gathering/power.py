import socket,sys
from threading import Thread
from time import sleep
from datetime import datetime
from IPython import embed
import csv

stage = 1

fields = ['Time Since Start', 'Pressure', 'Flow', 'Apparent Power Ext', 'Apparent Power Int', 'Active Power Ext', 'Active Power Int', 'Reactive Power Ext', 'Reactive Power Int', 'World Time']
toWrite = ""
with open('power.csv', "w") as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow(fields)

address = "172.21.4.150"
#address = "10.2.153.155"
port = 9998
# Create a TCP/IP socket
socket.setdefaulttimeout(30000)
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

server_address = (address, port)
print (sys.stderr, 'starting up on %s port %s' % server_address)

sock.bind(server_address)

sock.listen(1)
old = ""

while True:
    try:
        print(sys.stderr, 'waiting for a connection')
        connection, client_address = sock.accept()
        print("waiting..")

        try:
            print (sys.stderr, 'connection from', client_address)

            while True:
                data = connection.recv(80).decode()
                if data != old:
                    try:
                        toWrite = data.split(",")
                        print(toWrite)
                        toWrite.append(datetime.now())
                        if len(data) == 80:
                            with open('power.csv', "a", newline = "") as csv_file:
                                    writer = csv.writer(csv_file)
                                    rows = [toWrite]
                                    writer.writerows(rows)
                    except Exception as e:
                        print(e)
                    old = data


        except KeyboardInterrupt:
            connection.close()
            break

        except:
            ye = "man"

    except KeyboardInterrupt:
        connection.close()
        break

sock.close()
