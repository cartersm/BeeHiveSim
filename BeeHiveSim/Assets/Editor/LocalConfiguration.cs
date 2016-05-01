using System.Collections.Generic;

namespace Assets.Editor
{
    public class LocalConfiguration
    {
        public Dictionary<int, Dictionary<int, Cell>> Config { get; set; }
        private Point3D _location;

        public LocalConfiguration(BeeHiveGrid grid, Point3D location)
        {
            ConstructConfig(grid, location);
            this._location = location;
        }

        private void ConstructConfig(BeeHiveGrid grid, Point3D location)
        {
            // TODO: construct local config from Grid and Location
            this.Config = new Dictionary<int, Dictionary<int, Cell>>();
            throw new System.NotImplementedException();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            throw new System.NotImplementedException();
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}