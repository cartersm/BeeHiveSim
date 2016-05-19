using System.Text;

namespace Assets.Algorithm
{
    /// <summary>
    /// A LocalConfiguration. This is the set of directly or diagonally adjacent cells to a given bee.
    /// Used to sense the immediate area and make a decision on whether to place a brick and what type to place.
    /// </summary>
    public class LocalConfiguration
    {
        public Cell[,] Config { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config">A 2D array of cells, aligned [below me, on the same level as me, above me]</param>
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

            var other = (LocalConfiguration)obj;
            //return ConfigsEqual(this.Config, other.Config);
            // TODO: this is just checking strings. It should do an elementwise check on the cells.
            return this.ToString().Equals(other.ToString());
        }

        // TODO: FIXME
        private static bool ConfigsEqual(Cell[,] config, Cell[,] config2)
        {
            if (config.Rank != config2.Rank || config.Length != config2.Rank) return false;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    if (config[i, j] != null && !config[i, j].Equals(config2[i, j])) return false;
                }
            }
            return true;
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
                        hash += ((i * j) ^ (this.Config[i, j].BrickType + 3) - i * j);
                    }
                }
            }
            return hash;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Local Config:");
            for (var i = 0; i < 3; i++)
            {
                sb.Append("\n  ");
                for (var j = 0; j < 7; j++)
                {
                    if (this.Config[i, j] == null) continue;
                    sb.Append(this.Config[i, j].BrickType);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
    }
}
