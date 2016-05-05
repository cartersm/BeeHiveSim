namespace Assets.Editor
{
    public class Cell
    {
        public bool IsOccupied { get; set; }
        public int BrickType { get; set; }
        public Point3D Location;

        public Cell(Point3D location)
        {
            this.IsOccupied = false;
            this.Location = location;
            this.BrickType = 0;
        }

        // Extends base constructor to take indices instead of a point
        public Cell(int i, int j, int k) : this(new Point3D(i, j, k))
        {
            // unused
        }
    }
}