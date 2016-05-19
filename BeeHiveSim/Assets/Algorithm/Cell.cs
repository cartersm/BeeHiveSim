using Assets.Graphic;

namespace Assets.Algorithm
{
    /// <summary>
    /// A single cell in the Grid.
    /// </summary>
    public class Cell
    {
        public bool IsOccupied { get; set; }
        public int BrickType { get; set; }
        public Point3D Location { get; set; }

        /// <summary>
        /// Constructor for a cell with only a BrickType specified. Used for generating initial LocalConfigurations.
        /// </summary>
        /// <param name="brickType">TH type of brick this cell has.</param>
        public Cell(int brickType)
        {
            this.IsOccupied = true;
            this.Location = new Point3D();
            BrickType = brickType;
        }

        /// <summary>
        /// Base Constructor. Constructs a cell at the given location, unoccupied and with no brick type.
        /// </summary>
        /// <param name="location">The location of the cell in the grid.</param>
        public Cell(Point3D location)
        {
            this.IsOccupied = false;
            this.Location = location;
            this.BrickType = 0;
        }

        /// <summary>
        /// Extends base constructor to take indices instead of a point.
        /// </summary>
        /// <param name="x">The x-value of the cell.</param>
        /// <param name="y">The y-value of the cell.</param>
        /// <param name="z">The z-value of the cell.</param>
        public Cell(int x, int y, int z) : this(new Point3D(x, y, z))
        {
            // no further implementation required
        }

        /// <summary>
        /// Compares equality on BrickType only. Used internally by LocalConfigurations.
        /// </summary>
        /// <param name="other">The cell to compare against.</param>
        /// <returns></returns>
        protected bool Equals(Cell other)
        {
            return BrickType == other.BrickType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            // HashCode implementation is not required here.
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
    }
}
