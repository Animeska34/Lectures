import msvcrt
import os

def ClearConsole():
    os.system("CLS")

def Pause():
    os.system("PAUSE")

class Selector:
    def __init__(this, e = [], d = 0, r = False):
        this.elements = e
        this.default = d
        this.escReturn = r

    def Show(this):
        sel = this.default
        size = len(this.elements)
        print("\033[s")
        while True:
            for i in range(size):
                if(i == sel):
                    print("\033[1;30;44m[" + this.elements[i] + "]\033[0;0m")
                else:
                    print("\033[1;34;0m " + this.elements[i] + " \033[0;0m")
            key = str(msvcrt.getch())
            if key == "b'H'":
                sel-=1
                if sel < 0:
                    sel = size - 1
            elif key == "b'P'":
                sel+=1
                if sel >= size:
                    sel = 0
            elif key == "b'\\r'":
                return this.elements[sel]
            elif key == "b'\\x1b'" and this.escReturn:
                return ""
            print("\033[u")