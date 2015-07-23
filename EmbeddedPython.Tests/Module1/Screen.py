import sys
import os
import pygame

class Screen:
    screen = None;

    def __init__(self):
        found = False
        
        drivers = ['directfb', 'fbcon', 'svgalib']
        for driver in drivers:
            if not os.getenv('SDL_VIDEODRIVER'):
                os.putenv('SDL_VIDEODRIVER', driver)
            try:
                pygame.display.init()
                found = True
                break
            except pygame.error:
                continue
            
        # size = (pygame.display.Info().current_w, pygame.display.Info().current_h)
        # self.screen = pygame.display.set_mode(size, pygame.FULLSCREEN | pygame.NOFRAME | pygame.HWSURFACE | pygame.DOUBLEBUF)
        self.screen = pygame.display.set_mode((800, 480), pygame.NOFRAME)
        
        # pygame.mouse.set_visible(0)
        
        self.screen.fill((0, 0, 0))
        pygame.display.update()
        
        pygame.font.init()
        
    def __del__(self):
        pygame.display.quit()

    def get_display(self):
        return pygame.display

    def get_screen(self):
        return self.screen
    
    def create_surface(self, width, height):
        return pygame.Surface((width, height))
