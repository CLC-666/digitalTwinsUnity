import socket

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(('172.21.0.90', 2000))

for i in range(51):
    sock.sendall(('444;#RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo=' + '666' + ';#CNo=' + '1003' + ';#Aux1Int=' + '1' + ';\r').encode())
    data = sock.recv(1024)
