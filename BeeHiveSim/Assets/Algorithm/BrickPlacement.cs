namespace Assets.Editor
{
    public class BrickPlacement
    {
        public int BrickType { get; private set; }
        public double Chance { get; private set; }

        public BrickPlacement(int brickType, double chance)
        {
            this.BrickType = brickType;
            this.Chance = chance;
        }
    }
}