from RecursiveShadowCasting import Fov_RSC


fov = Fov_RSC(9, 9)
testmap = [[False, False, False, False, False, False, False, False, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, True, True, True, True, True, True, True, True, False],
           [False, False, False, False, False, False, False, False, False]]
result = fov.Calculate_Sight(testmap, 3, 3, 4)
s = ''
for y in range(9):
    for x in range(9):
        if result[x][y]:
            if testmap[x][y]:
                s += '.'
            else:
                s += '#'
        else:
            s += '▒'
    s += '\n'
print(s)
