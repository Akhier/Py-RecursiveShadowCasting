from RecursiveShadowCasting import Fov_RSC


fov = Fov_RSC(9, 9)
testmap = [[True for y in range(9)] for x in range(9)]
for y in range(9):
    for x in range(9):
        if x == 0 or y == 0 or x == 8 or y == 8:
            testmap[x][y] = False
result = fov.Calculate_Sight(testmap, 4, 4, 4)
s = ''
for y in range(9):
    for x in range(9):
        if result[x][y]:
            if x == 2 and y == 8:
                print(testmap[x][y])
            if testmap[x][y]:
                s += '.'
            else:
                s += '#'
        else:
            s += '▒'
    s += '\n'
print(s)
