import socket
from threading import Thread
import time

'''
data = [assembly station, conveyor start, module position, carrier released, conveyor end, carrierID]
'''
data = ['1,0,0,0,0,1', '1,1,0,0,0,1', '1,0,1,0,0,1', '1,0,1,1,0,1', '1,0,0,0,1,1']
toSend = ""

def prefData():
    global data
    global toSend
    counter = 0
    printed = False

    while True:
        var = round(time.time())
        if counter == 5:
            counter = 0

        if var % 5 == 0 and printed == False:
            toSend = data[counter]
            counter += 1
            printed = True

        if var % 5 != 0:
            printed = False


def main():
    global toSend

    s = socket.socket()
    s.connect(("172.21.4.152", 9997))

    while True:
        print("sending", toSend)
        s.send(toSend.encode())



if __name__ == '__main__':
    p1 = Thread(target=prefData, args=())
    p2 = Thread(target=main, args=())

    p1.start()
    p2.start()

    p1.join()
    p2.join()
