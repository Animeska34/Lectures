class Test:
    def __init__(this, num):
        this.x = num

    def __str__(this):
        return str(this.x)


class Test2(Test):
    def __init__(this, num):
        super().__init__(num)
        this.y = pow(num, 2)

    def __str__(this):
        return super().__str__() + "\n" + str(this.y)


print(str(Test2(5)))
input()
