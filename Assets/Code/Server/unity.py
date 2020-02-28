import socket


s = socket.socket()
host = '172.21.4.152'
port = 9997

s.connect((host, port))


while True:
    data = s.recv(12)
    print(data.decode())
