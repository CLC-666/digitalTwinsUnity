import socket


sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(("172.21.4.152",9999))

sock.sendall("hello".encode())
while True:
    print(sock.recv(10))
    # print(sock.recv(1024).decode())
