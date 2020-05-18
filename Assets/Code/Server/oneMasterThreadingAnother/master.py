from threading import Thread
from time import time
import socket

# 1 : magFront
# 2 : manual
# 3 : camInspect
# 4 : codesys1
# 5 : codesys2
# 6 : magBack
# 7 : pressing
# 8 : heating


PLCDATA = {1: magFront, 2: manual, 3: camInspect, 4: codesys1, 5: codesys2, 6: magBack, 7: pressing, 8: heating}
#data = [assembly station, conveyo]r start, module position, carrier released, conveyor end, carrierID, aux1, aux2, aux3]
magFront = ["1,0,0,0,0,0,0,0,0", "1,1,"]


def TCPclient(IP, PORT, PLC_NUMBER):
    global PLCDATA
    s = socket.socket()
    ADDRESS = ((IP, PORT))
    s.connect(ADDRESS)
    send


if __name__ == '__main__':
    p1 = Thread(target=hello, args=["hello"])
    p2 = Thread(target=hello, args=["hello2"])

    p1.start()
    p2.start()

    p1.join()
    p2.join()
