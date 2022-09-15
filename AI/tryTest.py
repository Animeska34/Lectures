from decimal import DivisionByZero


try:
    0/0
except (ZeroDivisionError):
    print("division by zero err")
except:
    print("other err")
finally:
    print("exec always")
