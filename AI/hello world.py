from os import system

# inserts user defined adjective to string
s = "Hello World"
while (1):
    print("Input adjective")
    i = input()
    i = i[0].upper() + i[1:]
    print(s[:5] + ", " + i + s[5:])
    input()
    system("CLS")
