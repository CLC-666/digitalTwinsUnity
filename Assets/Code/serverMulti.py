import socket, sys, struct
import threading

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

class server:
    def __init__(self):
        global sock
        address = "172.20.10.5"
        port = 6000
        server_address = (address, port)
        history = ""
        leftOverB = []
        leftOverE = []
        socket.setdefaulttimeout(30000)
        print (sys.stderr, 'starting up on %s port %s' % server_address)
        sock.bind(server_address)
        sock.listen(1)

    def run(self):
        while True:
            try:
                print(sys.stderr, 'waiting for a connection')
                connection, client_address = sock.accept()
                print("waiting..")
                try:
                    print (sys.stderr, 'connection from', client_address)

                    while True: #station, firstinduc, secondInduc, carrierReleased, thirdInduc, carrierid
                        data = connection.recv(64)
                        print(data.decode())
                        if not data:
                            break
                            connection.close()

                except KeyboardInterrupt:
                    connection.close()

                except:
                    ye = "man"
            except:
                break

        sock.close()
        threading.Thread.exit()

client = []
for i in range(1):
    i = server()
    client.append(threading.Thread(target=i.run, args=())
    client[0].daemon = True
    client[0].start()
    print("done")
