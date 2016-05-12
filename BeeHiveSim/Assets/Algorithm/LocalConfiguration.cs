using System.Text;
using UnityEngine;

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

            var other = (LocalConfiguration)obj;
            //return ConfigsEqual(this.Config, other.Config);
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

        private static string MultiDArrayToString(Cell[,] cells)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    var c = cells[i, j];
                    if (c == null) continue;
                    sb.Append(c.BrickType);
                    sb.Append(",");
                }
                sb.Append("\n");
            }
            return sb.ToString();
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