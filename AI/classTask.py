import PyCUI

class Human:
    def __init__(this, n):
        this.name = n


class Student(Human):
    marks = []

    def __str__(this):
        return this.name + ": " + str(this.Average())

    def __init__(this, n, m):
        super().__init__(n)
        this.marks = m

    def Average(this):
        if len(this.marks) > 0:
            x = 0
            for item in this.marks:
                x += item
            return x/len(this.marks)
        return 0


class College:
    students = []

    def Enter(this, s):
        this.students.append(s)

    def Get(this, n):
        for item in this.students:
            if item.name == n:
                return item

    def Best(this):
        if len(this.students) <= 0:
            return "none"
        best = this.students[0]
        avg = best.Average()
        for item in this.students:
            x = item.Average()
            if x > avg:
                avg = x
                best = item
        return best

    def SetMark(this, n, m):
        for item in this.students:
            if item.name == n:
                item.marks.append(m)


c = College()
while True:
    print("Please, select an option:")
    menu = ["Add student", "Add mark", "Best student"]
    i = PyCUI.Selector(menu).Show()

    if i == menu[0]:
        print("enter name")
        c.Enter(Student(input(), []))
    elif i == menu[1]:
        print("enter name")
        n = input()
        print("enter mark")
        c.SetMark(n, int(input()))
    else:
        print(str(c.Best()))
        PyCUI.Pause()
    
    
    PyCUI.ClearConsole()
