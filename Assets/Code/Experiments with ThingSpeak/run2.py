import requests
import time

url = "https://api.thingspeak.com/update?api_key=50TF1SQDRAO6Y63H&"

counter = 1
while True:
    field1 = counter
    data = requests.get(url + "field1=" + str(field1))
    time.sleep(1)
    print(data)
    counter += 1