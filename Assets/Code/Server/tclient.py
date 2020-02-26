# Python TCP Client A
import socket
from datetime import datetime
from time import sleep

host = "172.21.4.151"
port = 9997
BUFFER_SIZE = 1024
unityOption = input("tcpClientA: Enter message/ Enter exit:")

tcpClient = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
tcpClient.connect((host, port))

while True:
    print("operational")
    if unityOption == "no":
        tcpClient.send(str(datetime.now()).encode())

    if unityOption == "yes":
        data = tcpClient.recv(BUFFER_SIZE)
        print (" Client2 received data:", data.decode())

    sleep(0.5)

tcpClient.close()
