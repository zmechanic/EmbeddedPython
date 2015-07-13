import sys
import os
import Satellite

def pass_value_through(v):
    return v

def aaa(v):
    return Satellite.satellite_function(v) + 100

def bbb(v):
    return sys.version

class MyClass:
    def __init__(self):
        self._x = 12

    def doSomething(self):
        self._x += 1
