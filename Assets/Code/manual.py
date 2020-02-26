import socket
from threading import Thread
from time import sleep


sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# sock.connect(("172.21.4.152",9999))
sock.connect(("192.168.1.86",9999))

command = ''

def send():
    global command
    while(True):
        try:
            sock.sendall(command.encode())
            sleep(0.1)
        except KeyboardInterrupt:
            break

def choose():
    global command
    while(True):
        try:
            command = input("command? ")

        except KeyboardInterrupt:
            break


if __name__ == '__main__':
    p1 = Thread(target=send, args=())
    p2 = Thread(target=choose, args=())

    p1.start()
    p2.start()

    p1.join()
    p2.join()
