using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 0 = up
 * 1 = left
 * 2 = right
 * 3 = down
 * 4 = a = cross
 * 5 = b = circle
 * 6 = x = square
 * 7 = y = triangle
 * 8 = lPad
 * 9 = rPad
 * 10 = l1
 * 11 = l2
 * 12 = r1
 * 13 = r2
 * 14 = select
 * 15 = start
 * 16 = lStick
 * 17 = rStick
 */

namespace TouchGrass
{
    internal enum Commands
    {
        UP = 0,
        LEFT = 1,
        RIGHT = 2,
        DOWN = 3,
        A = 4,
        B = 5,
        X = 6,
        Y = 7,
        LPAD = 8,
        RPAD = 9,
        L1 = 10,
        L2 = 11,
        R1 = 12,
        R2 = 13,
        SELECT = 14,
        START = 15,
        LSTICK = 16,
        RSTICK = 17
    }

    internal enum CommandDirection
    {
        IDLE = 0,
        UP = 1,
        UPRIGHT = 2,
        RIGHT = 3,
        DOWNRIGHT = 4,
        DOWN = 5,
        DOWNLEFT = 6,
        LEFT = 7,
        UPLEFT = 8
    }
}
