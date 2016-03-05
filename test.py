from RecursiveShadowCasting import Fov_RSC

width = 9
height = 9
fov = Fov_RSC(width, height)
testmap = [[True for y in range(height)] for x in range(width)]
for y in range(height):
    for x in range(width):
        if x == 0 or y == 0 or x == width - 1 or y == height - 1:
            testmap[x][y] = False
result = fov.Calculate_Sight(testmap, 4, 4, 4)
s = ''
for y in range(height):
    for x in range(width):
        if result[x][y]:
            if testmap[x][y]:
                s += '.'
            else:
                s += '#'
        else:
            s += '▒'
    s += '\n'
print(s)
