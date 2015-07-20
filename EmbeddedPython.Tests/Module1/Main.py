import sys
import os
import pygame

class MyClass:
    def __init__(self):
        self._x = 12

    def doSomething(self):
        pygame.mixer.init()
        sound = pygame.mixer.Sound("C:/222/Papero_1.wav")
        sound.play()
        return
