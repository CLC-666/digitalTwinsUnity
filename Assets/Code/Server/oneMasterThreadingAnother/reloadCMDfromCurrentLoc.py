import os


while True:
    if input("now?") == "y":
        os.system("start /d \"" + str(os.getcwd()) + "\"")
