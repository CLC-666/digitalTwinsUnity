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

# takes the first argument from command prompt as IP address
IP_address = "172.21.4.152"

# takes second argument from command prompt as port number
Port = 9997


server.bind((IP_address, Port))

"""
listens for 100 active connections. This number can be
increased as per convenience.
"""
server.listen(100)

list_of_clients = []

def clientthread(conn, addr):
    global unity
    counter = 0
    # sends a message to the client whose user object is conn
    # conn.send("Welcome to this chatroom!")

    while True:

            try:
                # time.sleep(0.1)
                message = conn.recv(11)
                if message:

                    """prints the message and address of the
                    user who just sent the message on the server
                    terminal"""
                    # print ("<" + addr[0] + "> " + message.decode(), counter)
                    counter += 1
                    # Calls broadcast function to send message to all
                    message_to_send = message
                    # print("trying in while still")
                    if addr[0] == '172.21.4.1':
                        # print(counter)
                        if counter % 5 == 0:
                            # print(message.decode())
                            broadcast(message_to_send, conn)
                            counter = 0
                    #     var = round(time.time()*1000)
                    #     if var % 2:
                    #         broadcast(message_to_send, conn)

                    if addr[0] != IP_address and addr[0] != "172.21.4.1":
                        broadcast(message_to_send, conn)



                else:
                    """message may have no content if the connection
                    is broken, in this case we remove the connection"""
                    remove(conn)

            except:
                continue

"""Using the below function, we broadcast the message to all
clients who's object is not the same as the one sending
the message """
def broadcast(message, connection):
    global unity
    try:
        list_of_clients[unity].send(message)
        print("sent", message)
    except Exception as e:
        # print(e)
        list_of_clients[unity].close()

        # if the link is broken, we remove the client
        remove(clients)

"""The following function simply removes the object
from the list that was created at the beginning of
the program"""
def remove(connection):
    if connection in list_of_clients:
        list_of_clients.remove(connection)


while True:

    """Accepts a connection request and stores two parameters,
    conn which is a socket object for that user, and addr
    which contains the IP address of the client that just
    connected"""
    conn, addr = server.accept()

    """Maintains a list of clients for ease of broadcasting
    a message to all available people in the chatroom"""
    list_of_clients.append(conn)

    # prints the address of the user that just connected
    print (addr[0] + " connected")
    if addr[0] == IP_address:
        print("unity init")
        unity = list_of_clients.index(conn)

    # creates and individual thread for every user
    # that connects
    start_new_thread(clientthread,(conn,addr))

conn.close()
server.close()
