def GetBiggest(i):
    if (len(i) >= 1):
        b = i[0]
        for n in i:
            if (n > b):
                b = n
        return b
    else:
        return 0


print("Enter number number")
n = int(input())
l = []
r = []
print("Enter numbers")
for i in range(n):
    l.append(int(input()))
    if (len(l) >= 3):
        if (l[1] > l[2] and l[1] > l[0]):
            r.append(l[1])
        l = []
print("Biggest neighbours: ")
print(GetBiggest(r))
input()
