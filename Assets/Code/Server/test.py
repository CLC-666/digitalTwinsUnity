from threading import Thread
from time import sleep
from IPython import embed

buffer = {}

class hello(Thread):
    def __init__(self,cat):
        Thread.__init__(self)
        self.cat = cat

    def run(self):
        global buffer
        global counter
        c = 0
        while True:
            buffer[counter] = c
            c += 1


dogs = []
counter = 0

while counter < 10:
    print("loop")
    dogs.append(hello(cat=counter))
    print("append")
    dogs[counter].start()
    print("start", counter)
    counter += 1
    if counter == 9:
        # for i in dogs:
        #     i.join()

        print(Thread.activeCount())
