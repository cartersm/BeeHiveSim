
namespace Assets.Graphic
{
    public class Point3D
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Point3D(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
