# Python program to implement server side of chat room.
import socket
import select
import sys
from _thread import *
import time

"""The first argument AF_INET is the address domain of the
socket. This is used when we have an Internet Domain with
any two hosts The second argument is the type of socket.
SOCK_STREAM means that data or characters are read in
a continuous flow."""
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

unity = ""
sendBuffer = []

# takes the first argument from command prompt as IP address
# IP_address = "172.21.4.152"
IP_address = "10.2.254.178"

# takes second argument from command prompt as port number
Port = 9992


server.bind((IP_address, Port))

server.listen(100)

list_of_clients = []

def clientthread(conn, addr):
    global unity
    counter = 0
    old = ""
    while True:
            try:
                broadcast(message_to_send, conn)
                message = conn.recv(17)

                if message:
                    message_to_send = message

                    if addr[0] != IP_address and message.decode() != old:
                        broadcast(message_to_send, conn)
                        old = message.decode()

                else:
                    if addr[0] != IP_address:
                        remove(conn)

            except:
                continue

def broadcast(message, connection):
    global unity
    try:
        print(list_of_clients)
        list_of_clients[unity].send("HELLOOOOO")
        print("sent", message)
    except Exception as e:
        # print(e)
        list_of_clients[unity].close()
        remove(clients)


def remove(connection):
    if connection in list_of_clients:
        list_of_clients.remove(connection)


while True:
    conn, addr = server.accept()
    list_of_clients.append(conn)

    print (addr," connected")
    if addr[0] == IP_address:
        print("unity init")
        unity = list_of_clients.index(conn)

    start_new_thread(clientthread,(conn,addr))

conn.close()
server.close()
