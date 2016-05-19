namespace Assets.Algorithm
{
    /// <summary>
    /// A wrapper class containing the type of brick to place and the percentage chance of placing it.
    /// </summary>
    public class BrickPlacement
    {
        public int BrickType { get; private set; }
        public double Chance { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="brickType">The type of brick to place.</param>
        /// <param name="chance">The chance of placing that brick.</param>
        public BrickPlacement(int brickType, double chance)
        {
            this.BrickType = brickType;
            this.Chance = chance;
        }

        public override string ToString()
        {
            return "BrickPlacement: Type: " + BrickType + ", Chance: " + Chance;
        }
    }
}
