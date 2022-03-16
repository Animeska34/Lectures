import tkinter as tk
window = tk.Tk()
entry = tk.Entry(width=40, foreground="black")
entry.insert(0, "Koks jūsų vardas?")
entry.pack()
window.mainloop()