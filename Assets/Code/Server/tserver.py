import socket
from threading import Thread
from socketserver import ThreadingMixIn
from IPython import embed
from time import sleep

plcs = ["172.21.2.1","172.21.1.1","172.21.3.1","172.21.4.1","172.21.5.1","172.21.6.1"]
host_name = socket.gethostname()
unity = socket.gethostbyname(host_name)

buffer = []

# Multithreaded Python server : TCP Server Socket Thread Pool
class ClientThread(Thread):

    def __init__(self,ip,port):
        Thread.__init__(self)
        self.ip = ip
        self.port = port
        self.unityOption = False
        print( "[+] New server socket thread started for " + ip + ":" + str(port))

    def run(self):
        global buffer

        if self.unityOption == False: #non unity client
            while True :
                data = conn.recv(12)
                if "," in data.decode():
                    print ("Server received data from:", self.ip, data.decode())
                    buffer.append(str(data.decode()))  # echo

        if self.unityOption == True:
            print("unity client selected")
            while True:
                print("in true")
                if len(buffer) > 0:
                    for i in range(len(buffer)):
                        print("sending data", buffer[i], "buffer is", len(buffer))
                        sleep(0.5)
                        conn.send(buffer[i].encode())
                        buffer.pop(0)

    def unity(self, option):
        self.unityOption = option

    def ifunity(self):
        return self.unityOption

# Multithreaded Python server : TCP Server Socket Program Stub
TCP_IP = '0.0.0.0'
TCP_PORT = 9997
BUFFER_SIZE = 12  # Usually 1024, but we need quick response

tcpServer = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
tcpServer.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
tcpServer.bind((TCP_IP, TCP_PORT))
threads = []
connections = {}
counter = 0
fail = False

while True:
    tcpServer.listen(5)
    print ("Multithreaded Python server : Waiting for connections from TCP clients...")
    print(threads)
    (conn, (ip,port)) = tcpServer.accept()
    # try:
    #     if connections[ip] == 2:
    #         print("ye")
    # except:
    #     connections[ip] = port
    newthread = ClientThread(ip,port)
    newthread.start()
    threads.append(newthread)
    # threads[ip].join()
    # counter += 1



    # try:
    #     if threads[ip] != "":
    #         print("exiting thread", ip)
    #         threads[ip].exit()
    # except:
    #     print("starting new thread")
    #     newthread.start()
    #     threads[ip] = newthread
    #
    if ip == unity:
        newthread.unity(True)
    #     print("I JUST FOUND UNITY :D")
    # print("new guy", newthread.ifunity())
    # # embed()



for t in threads:
    print("joining")
    t.join()
