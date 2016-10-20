using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practical2
{
    /// <summary>
    /// A struct for storing a 2d coordinate
    /// </summary>
    public struct Coord
    {
        public int x, y;

        /// <summary>
        /// A struct for storing a 2d coordinate
        /// </summary>
        /// <param name="X">The x-value of the coordinate</param>
        /// <param name="Y">The y-value of the coordinate</param>
        public Coord(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
}
