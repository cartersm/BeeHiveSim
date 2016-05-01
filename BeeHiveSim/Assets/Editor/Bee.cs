using System.Collections.Generic;

namespace Assets.Editor
{
    public class Bee
    {
        public int Number { get; set; }
        public Point3D Location { get; set; }
        /* This is the genetic portion */
        public List<LocalConfiguration> LookupTable { get; private set; }

        public Bee(int number, Point3D location)
        {
            this.Number = number;
            this.Location = location;
        }

        /**
         * Senses the local configuration of cells around the bee's current location, 
         * and tries to find it in the lookup table.
         */
        public bool SenseEnvironment(LocalConfiguration config)
        {
            // TODO: sense local config and try to find it in the lookup table.
            throw new System.NotImplementedException();
        }
    }
}