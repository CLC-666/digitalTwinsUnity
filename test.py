import socket


sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(('10.3.121.133',9999))

sock.sendall("hello".encode())
while True:
    print(sock.recv(10))
    # print(sock.recv(1024).decode())
