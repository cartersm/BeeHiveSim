namespace Assets.Algorithm
{
    public class LocalConfiguration
    {
        public Cell[,] Config { get; set; }

        public LocalConfiguration(Cell[,] config)
        {
            this.Config = config;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (LocalConfiguration) obj;
            return this.Config.Equals(other.Config);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.Config.GetHashCode();
        }
    }
}