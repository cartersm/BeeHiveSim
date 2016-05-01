namespace Assets.Editor
{
    public class Cell
    {
        public bool HasBrick { get; private set; }
        public Point3D Location;

        public Cell(Point3D location)
        {
            this.Location = location;
            this.HasBrick = false;
        }
    }
}