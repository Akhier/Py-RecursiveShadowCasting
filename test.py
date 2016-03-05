from RecursiveShadowCasting import Fov_RSC


fov = Fov_RSC(5, 5)
testmap = [[False, False, False, False, False],
           [False, True, True, True, False],
           [False, True, True, True, False],
           [False, True, True, True, False],
           [False, False, False, False, False]]
result = fov.Calculate_Sight(testmap, 3, 3, 10)
s = ''
for y in range(5):
    for x in range(5):
        if result[x][y]:
            if testmap[x][y]:
                s += '.'
            else:
                s += '#'
        else:
            s += '~'
    s += '\n'
print(s)
