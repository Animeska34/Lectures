import tkinter as tk
import random

colors = ["#FA8072", "#FFB6C1", "#FFA07A", "#FFD700", "#EE82EE", "#ADFF2F","#00FFFF"]

def SetRandom():
    button["background"] = colors[random.randint(0, len(colors)-1)]

window = tk.Tk()
button = tk.Button(command = SetRandom, text="Spalvos keitimas čia")
button.pack()
SetRandom()
window.mainloop()
