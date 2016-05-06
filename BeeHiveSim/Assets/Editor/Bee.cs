using System.Collections.Generic;

namespace Assets.Editor
{
    public class Bee
    {
        public int Number { get; set; }
        public Point3D Location { get; set; }
        /* This is the genetic portion */
        public Dictionary<LocalConfiguration, BrickPlacement> LookupTable { get; private set; }

        public Bee(int number, Point3D location, Dictionary<LocalConfiguration, BrickPlacement> lookupTable)
        {
            this.Number = number;
            this.Location = location;
            this.LookupTable = lookupTable;
        }

        /**
         * Senses the local configuration of cells around the bee's current location, 
         * and tries to find it in the lookup table.
         */
        public BrickPlacement SenseEnvironment(LocalConfiguration config)
        {
            BrickPlacement retVal;
            return this.LookupTable.TryGetValue(config, out retVal) ? retVal : null;
        }
    }
}