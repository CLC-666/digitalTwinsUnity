import socket
from time import sleep
# One way control

serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serv.bind(('192.168.1.86', 9999))
serv.listen(5)

testData = ["First induction sensor_SERVER", "RFID_SERVER", "End induction sensor_SERVER"]
counter = 0

while True:

    conn, addr = serv.accept()
    from_client = ''

    while True:
        conn.send(testData[counter].encode())



    conn.close()
