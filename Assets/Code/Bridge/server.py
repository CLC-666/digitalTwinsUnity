import socket
from _thread import *
import sys

server = ""
port = 5555


s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

try:
    s.bind((server, port))
except socket.error as e:
    str(e)

s.listen(2)
print("Waiting for a connection, Server Started")

bigDataList = ["", "", "", ""]
old0 = ""
old1 = ""
old2 = ""
old3 = ""


def threaded_client(conn, player):
    # conn.send(str.encode(make_pos(pos[player])))
    global old0
    global old1
    global old2
    global old3
    global bigDataList

    reply = ""
    while True:
        try:
            if player != 3:
                data = str(conn.recv(11).decode())
                bigDataList[player] = data

                if not data:
                    print("Disconnected")
                    break

            if player == 3:
                if old0 != bigDataList[0]:
                    print(bigDataList[0], old0)
                    conn.sendall((bigDataList[0]).encode())
                    old0 = bigDataList[0]

                if old1 != bigDataList[1]:
                    print(bigDataList[1], old1, len(old1), len(bigDataList[1]))
                    conn.sendall((bigDataList[1]).encode())
                    old1 = bigDataList[1]

                if old2 != bigDataList[2]:
                    print(bigDataList[2], old2)
                    conn.sendall((bigDataList[2]).encode())
                    old2 = bigDataList[2]

                # if old3 != bigDataList[3]:
                #     conn.sendall(str(player) + bigDataList[3].encode())
                #     old3 = bigDataList[3]

        except Exception as e:
            print(e)
            break

    print("Lost connection")
    conn.close()


currentPlayer = 0
while True:
    conn, addr = s.accept()
    print("Connected to:", addr)

    start_new_thread(threaded_client, (conn, currentPlayer))
    currentPlayer += 1
