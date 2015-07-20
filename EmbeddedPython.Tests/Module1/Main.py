import sys
import os
import pygame

class MyClass:
    def __init__(self):
        self._sound = None

    def loadSounds(self):
        pygame.mixer.init()
        self._sound = pygame.mixer.Sound("Papero_1.wav")
        return

    def doSomething(self):
        self._sound.play()
        return
