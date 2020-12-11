from network import Network
from threading import Thread

clientNumber = 0
send = ""


def getDataFromUni():
    global send
    try:
        n = Network("10.14.129.55", 5555)
        print("connected to cp lab")
        while True:
            print(n.receive())
            send = n.receive()
    except Exception as e:
        print(e)


def sendDataToUnity():
    global send
    old = ""
    try:
        s = Network("10.54.63.134", 5555)
        print("connected to unity")
        while True:
            s.sender(send)
    except Exception as e:
        print(e)


if __name__ == '__main__':
    t1 = Thread(target=getDataFromUni, args=())
    t2 = Thread(target=sendDataToUnity, args=())

    t1.start()
    t2.start()

    t1.join()
    t2.join()
