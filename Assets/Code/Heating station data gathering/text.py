import csv


fields = ['Order Number', 'Carrier ID', 'Actual Heat', 'Target Heat', 'Actual Time', 'Target Time', 'Full Station Time']

# order number, carrier ID, actual heat, target heat, actual time, target time, full station time
rows = [[2345, 4, 45, 50, 0, 5000, 245]]

filename = "test.csv"

with open('test.csv', "w") as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow(fields)
        writer.writerows(rows)

while True:
    ye ="man"
