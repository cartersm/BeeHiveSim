namespace Assets.Editor
{
    public class Cell
    {
        public bool IsOccupied { get; set; }
        public int BrickType { get; private set; }
        public Point3D Location;

        public Cell(Point3D location)
        {
            this.IsOccupied = false;
            this.Location = location;
            this.BrickType = 0;
        }
    }
}