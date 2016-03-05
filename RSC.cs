using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recursive_Shadow_Casting___2d_square
{
    public class RSC
    {
        private bool[,] visibleMap;
        private int[,] opacityMap;
        private int mapXLimit, mapYLimit, playerX, playerY, visualRange;
        public bool[,] calculateSight(int mapxlimit, int mapylimit, int[,] opacitymap, int playerx, int playery, int visualrange)
        {
            mapXLimit = mapxlimit;
            mapYLimit = mapylimit;
            opacityMap = opacitymap;
            playerX = playerx;
            playerY = playery;
            visualRange = visualrange;

            visibleMap = new bool[mapXLimit, mapYLimit];
            for (int octant = 1; octant < 9; octant++)
            {
                scan(1, octant, 1.0, 0.0);
            }
            return visibleMap;
        }

        private double getSlope(double x1, double y1, double x2, double y2)
        {
            return (x1 - x2) / (y1 - y2);
        }

        private double getSlopeInv(double x1, double y1, double x2, double y2)
        {
            return (y1 - y2) / (x1 - x2);
        }

        private bool testCell(int x, int y, int cellState, int depth)
        {
            try
            {
                if (!isVisible(playerX, playerY, x, y)) { throw new Exception(); }
                return opacityMap[x, y] == cellState;
            }
            catch
            {
                return false;
            }
        }

        void scan(int depth, int octant, double startSlope, double endSlope)
        {
            int x = 0;
            int y = 0;

            switch (octant)
            {
                case 1:
                    y = playerY - depth;
                    x = playerX - Convert.ToInt32(startSlope * Convert.ToDouble(depth));
                    if (x < 0) { break; }
                    if (y < 0) { break; }
                    if (x >= mapXLimit) { break; }
                    if (y >= mapYLimit) { break; }

                    while (getSlope(x, y, playerX, playerY) >= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)   //1 means cell is blocked
                            {
                                if (testCell(x - 1, y, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlope(x - .5, y + 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x - 1, y, 1, depth))
                                {
                                    startSlope = getSlope(x - .5, y - 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        x++;
                    }
                    x--;   //Step back as the last step of the while has taken it past the limit
                    break;

                case 2:
                    y = playerY - depth;
                    x = playerX + Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;

                    while (getSlope(x, y, playerX, playerY) <= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x + 1, y, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlope(x + 0.5, y + 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x + 1, y, 1, depth))
                                {
                                    startSlope = -getSlope(x + 0.5, y - 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        x--;
                    }
                    x++;
                    break;

                case 3:
                    x = playerX + depth;
                    y = playerY - Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;
                    while (getSlopeInv(x, y, playerX, playerY) <= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1) //cell blocked
                            {
                                if (testCell(x, y - 1, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlopeInv(x - 0.5, y - 0.5, playerX, playerY));
                                }
                            }
                            else //not blocked
                            {
                                if (testCell(x, y - 1, 1, depth))
                                {
                                    startSlope = -getSlopeInv(x + 0.5, y - 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        y++;
                    }
                    y--;
                    break;

                case 4:
                    x = playerX + depth;
                    y = playerY + Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break; ;
                    while (getSlopeInv(x, y, playerX, playerY) >= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x, y + 1, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlopeInv(x - 0.5, y + 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x, y + 1, 1, depth))
                                {
                                    startSlope = getSlopeInv(x + 0.5, y + 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        y--;
                    }
                    y++;
                    break;

                case 5:
                    y = playerY + depth;
                    x = playerX + Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;
                    while (getSlope(x, y, playerX, playerY) >= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x + 1, y, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlope(x + 0.5, y - 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x + 1, y, 1, depth))
                                {
                                    startSlope = getSlope(x + 0.5, y + 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        x--;
                    }
                    x++;
                    break;

                case 6:
                    y = playerY + depth;
                    x = playerX - Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;
                    while (getSlope(x, y, playerX, playerY) <= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x - 1, y, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlope(x - 0.5, y - 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x - 1, y, 1, depth))
                                {
                                    startSlope = -getSlope(x - 0.5, y + 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        x++;
                    }
                    x--;
                    break;

                case 7:
                    x = playerX - depth;
                    y = playerY + Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;
                    while (getSlopeInv(x, y, playerX, playerY) <= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x, y + 1, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlopeInv(x + 0.5, y + 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x, y + 1, 1, depth))
                                {
                                    startSlope = -getSlopeInv(x - 0.5, y + 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        y--;
                    }
                    y++;
                    break;

                case 8:
                    x = playerX - depth;
                    y = playerY - Convert.ToInt32((startSlope * Convert.ToDouble(depth)));
                    if (x < 0) break;
                    if (x >= mapXLimit) break;
                    if (y < 0) break;
                    if (y >= mapYLimit) break;
                    while (getSlopeInv(x, y, playerX, playerY) >= endSlope)
                    {
                        if (isVisible(playerX, playerY, x, y))
                        {
                            if (opacityMap[x, y] == 1)
                            {
                                if (testCell(x, y - 1, 0, depth))
                                {
                                    scan(depth + 1, octant, startSlope, getSlopeInv(x + 0.5, y - 0.5, playerX, playerY));
                                }
                            }
                            else
                            {
                                if (testCell(x, y - 1, 1, depth))
                                {
                                    startSlope = getSlopeInv(x - 0.5, y - 0.5, playerX, playerY);
                                }
                            }
                            visibleMap[x, y] = true;
                        }
                        y++;
                    }
                    y--;
                    break;
            }
            if (x < 0) x = 0;
            if (x >= mapXLimit) x = mapXLimit - 1;
            if (y < 0) y = 0;
            if (y >= mapYLimit) y = mapYLimit - 1;


            if (isVisible(playerX, playerY, x, y) & opacityMap[x, y] == 0)
            {
                scan(depth + 1, octant, startSlope, endSlope);
            }
        }

        protected bool isVisible(int x1, int y1, int x2, int y2)
        {
            try
            {
                int i = opacityMap[x1, y1];   //illegal values throw an error
                i = opacityMap[x2, y2];
                if (x1 == x2) { return Math.Abs(y1 - y2) <= visualRange; }
                if (y1 == y2) { return Math.Abs(x1 - x2) <= visualRange; }
                return (Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)) <= Math.Pow(visualRange, 2);
            }
            catch
            {
                return false;
            }
        }
    }
}
