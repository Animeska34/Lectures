import tkinter as tk
from tkinter import messagebox

window = tk.Tk();
window.title("Egzamono Uzduotis :)")
window.resizable(False, False)
label = tk.Label(text = "Iveskite du zodzius", width=30)
label.pack();
text = tk.StringVar()
entry = tk.Entry(width = 30, textvariable=text)
entry.pack();

def message():
   messagebox.showinfo("Du zodziai", "Du zodziai: " + text.get())

control = tk.Frame()
ok = tk.Button(text="Ok", master=control, width=5, command = message)
ok.pack(pady=2, padx=30, side = tk.RIGHT)
cncl = tk.Button(text="Cancel", master=control, width=5, command=window.destroy)
cncl.pack(pady=2, padx=30, side = tk.LEFT)

control.pack()

window.mainloop();
