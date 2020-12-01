import socket, time


s = socket.socket()

s.connect(('192.168.0.111', 9002))


dataArray = [[0,0,0,0,0]]
#no carrier, carrier at start, carrier at middle, carrier released, carrier at end
for i in range(5):
    data = [0,0,0,0,0]
    data[i] = 1
    dataArray.append(data)

var = input("ready? ")
start = False

if var == "yes":
    start = True

old = 0
new = 0

print(dataArray)
print(start)
if start == True:
    for i in range(10):
        for j in range(5):
            print(new, old)
            new = int(str(int(time.time()))[-1])
            while(new == old):
                pass
            if new % 5 and old != new == 0:
                # s.send(dataArray[j].encode())
                print(dataArray[j])
                old = new
