
import turtle

turtle.getscreen()


def drawAnyShape(sides, length):
    sides = int(sides)
    length = int(length)
    angle = 360/sides
    turtle.pendown()
    for i in range(sides):
        print(angle)
        turtle.right(angle)
        turtle.forward(length)

num = input("sides? and length?")
sides, length = num.split(',')
print(sides)
print(length)
drawAnyShape(sides, length)
turtle.exitonclick()
