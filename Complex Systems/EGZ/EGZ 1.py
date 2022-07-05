import tkinter as tk

window = tk.Tk()
window.resizable(False, False)

frame1 = tk.Frame(master=window, height=100, width=150, relief=tk.FLAT, bg="red")
frame1.grid(row=0,column=0)
frame2 = tk.Frame(master=window, height=100, width=150, relief=tk.FLAT, bg="yellow")
frame2.grid(row=0,column=1)
frame3 = tk.Frame(master=window, height=100, width=150, relief=tk.FLAT, bg="blue")
frame3.grid(row=0,column=2)
window.mainloop()
