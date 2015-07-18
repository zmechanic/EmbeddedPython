import sys
import os

def pass_value_through(v):
    return v

class MyClass:
    def __init__(self):
        self._x = 12

    def doSomething(self):
        self._x += 1
