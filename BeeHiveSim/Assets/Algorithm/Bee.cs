using System.Collections.Generic;
using Assets.Graphic;

namespace Assets.Algorithm
{
    /// <summary>
    /// A single bee, which stores its location and lookup table and can sense LocalConfigurations.
    /// </summary>
    public class Bee
    {
        public Point3D Location { get; set; }
        public Dictionary<LocalConfiguration, BrickPlacement> LookupTable { get; private set; }

        public Bee(Point3D location, Dictionary<LocalConfiguration, BrickPlacement> lookupTable)
        {
            this.Location = location;
            this.LookupTable = lookupTable;
        }

        /// <summary>
        /// Senses the local configuration of cells around the bee's current location and tries to find it in the lookup table.
        /// </summary>
        /// <param name="config">The LocalConfiguration to check against.</param>
        /// <returns></returns>
        public BrickPlacement SenseEnvironment(LocalConfiguration config)
        {
            BrickPlacement retVal;
            return this.LookupTable.TryGetValue(config, out retVal) ? retVal : null;
        }
    }
}
