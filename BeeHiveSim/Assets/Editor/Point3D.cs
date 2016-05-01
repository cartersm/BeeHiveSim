
namespace Assets.Editor
{
    public class Point3D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public Point3D(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
