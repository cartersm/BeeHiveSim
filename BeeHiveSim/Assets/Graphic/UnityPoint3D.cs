namespace Assets.Graphic
{
    public class UnityPoint3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public UnityPoint3D(float x = 0, float y = 0, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
