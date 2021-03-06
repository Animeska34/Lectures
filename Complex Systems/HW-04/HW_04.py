import tkinter as tk

window = tk.Tk()
window.title("Address Entry Form")
window.resizable(False, False)
frame = tk.Frame(relief = tk.SUNKEN, borderwidth=1)

def Form(name, pos):
    label = tk.Label(master = frame, text = name, width=15, anchor='e')
    entry = tk.Entry(master = frame, width = 35)
    label.grid(column = 0, row = pos)
    entry.grid(column = 1, row = pos)


Form("First Name:", 0)
Form("Last Name:", 1)
Form("Address Line 1:", 2)
Form("Address Line 2:", 3)
Form("City:", 4)
Form("State/Province:", 5)
Form("Postal Code:", 6)
Form("Country:", 7)

frame.pack()
control = tk.Frame()
cncl = tk.Button(text="Cancel", master=control)
cncl.pack(pady=2, padx=2, side = tk.LEFT)
ok = tk.Button(text="Submit", master=control)
ok.pack(pady=2, padx=2)
control.pack(anchor='e')
window.mainloop()