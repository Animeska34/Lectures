import tkinter as tk
import random

def SetRandom():
    label["text"] = random.randint(1, 6)

window = tk.Tk()
button = tk.Button(command = SetRandom, text="Kauliuko metimas", width=20, height=3)
button.pack()
label = tk.Label(width=20, height=3)
label.pack()

SetRandom()
window.mainloop()