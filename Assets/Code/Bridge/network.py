import socket


class Network:
    def __init__(self, server, port):
        self.client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.server = server
        self.port = port
        self.addr = (self.server, self.port)
        self.client.connect(self.addr)

    def receive(self):
        try:
            return self.client.recv(11).decode()
        except socket.error as e:
            print(e)

    def sender(self, sendString):
        try:
            return self.client.sendall(sendString.encode())
        except socket.error as e:
            print(e)
