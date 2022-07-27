import threading
import paho.mqtt.publish as publish
import string
from IPython import embed

f = open("thingspeakCred.txt", 'r')
mqtt_username, mqtt_client_ID, mqtt_password, channel_ID = f.readline().split(',')
f.close()

mqtt_host = "mqtt3.thingspeak.com"

t_transport = "websockets"
t_port = 80

topic = "channels/" + channel_ID + "/publish"

while (True):
    
    # build the payload string.
    payload = "field1=" + str(2) + "&field2=" + str(3)

    # attempt to publish this data to the topic.
    print ("Writing Payload = ", payload," to host: ", mqtt_host, " clientID= ", mqtt_client_ID, " User ", mqtt_username, " PWD ", mqtt_password)
    print(publish.single(topic, payload, hostname=mqtt_host, transport=t_transport, port=t_port, client_id=mqtt_client_ID, auth={'username':mqtt_username,'password':mqtt_password}))
    print("sent")
    
    