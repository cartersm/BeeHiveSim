
using System;

namespace Assets.Graphic
{
    public class Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x = 0, int y = 0, int z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return string.Format("Point3D: ({0}, {1}, {2})", X, Y, Z);
        }
    }
}
