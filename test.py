import socket


sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(('192.168.1.86',9999))

sock.sendall("hello".encode())
