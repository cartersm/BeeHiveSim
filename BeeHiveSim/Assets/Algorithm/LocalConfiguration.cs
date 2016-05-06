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
            var hash = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    if (this.Config[i, j] != null)
                    {
                        
                        hash += ((i*j)^(this.Config[i, j].BrickType +3) - i*j);
                    }
                }
            }
            return hash;
        }
    }
}