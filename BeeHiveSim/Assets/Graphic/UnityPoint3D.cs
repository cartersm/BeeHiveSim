namespace Assets.Graphic
{
    public class UnityPoint3D {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public UnityPoint3D(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
