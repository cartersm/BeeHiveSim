using System.Collections.Generic;

// TODO: think about how we want to structure this. It has to be customized beyond an array/list.
// Currently, I have a list of (3D) layers, each of which has an indexed map of maps of cells.
namespace Assets.Editor
{
    public class BeeHiveGrid
    {
        public List<Layer> Layers { get; private set; }

        private BeeHiveGrid()
        {
            this.Layers = new List<Layer>();
            // TODO: initialize each layer (20 of them?)
        }

        public class Layer
        {
            private Dictionary<int, Dictionary<int, Cell>> _cells;

            public Layer()
            {
                this._cells = new Dictionary<int, Dictionary<int, Cell>>();
                // TODO: initialize a grid of empty cells by filling the maps with the expected numbers of cells.
                // This structure is based on Sean's ant swarm graph-mapping.
                // Alternatively, use an internal Point3D on each cell to store its location
            }
        }
    }
}