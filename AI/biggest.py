l = list()


def GetBiggest():
    if (len(l) > 1):
        b = l[0]
        for n in l:
            if (n > b):
                b = n
        return b
    else:
        return 0


while 1:
    i = input()
    if (i == "0"):
        print(GetBiggest())
        break
    else:
        l.append(i)
input()
