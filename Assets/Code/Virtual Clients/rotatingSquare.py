import turtle
from math import *
from time import sleep
turtle.getscreen()


def shape(sides, length):
    for i in range(1000):
        sides = i + 2
        angList = findPoints(sides)
        turtle.pendown()
        for i in range(sides):
            x = r * sin(radians(angList[i]))
            y = r * cos(radians(angList[i]))
            turtle.goto(x,y)
            # sleep(0.2)
        # turtle.goto(xList[0], yList[0])


def findPoints(sides):
    angList = []
    for i in range(sides):
        angList.append((360/sides) * (i+1))
    print(angList)
    return angList

turtle.speed(1000)
r = 100
a = 0
b = 0

turtle.setx(0)
turtle.penup()
turtle.sety(-r)
turtle.pendown()
turtle.circle(r)
turtle.pu()
turtle.home()


shape(5,3)
# for i in range(360):
#     print(i)
#     turtle.setx(r * sin(i))
#     turtle.sety(r * cos(i))
#     sleep(0.2)

turtle.exitonclick()

# (x)**2 + (y)**2 = r**2




#
