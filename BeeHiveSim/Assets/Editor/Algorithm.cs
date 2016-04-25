using System.Collections;
using System.Collections.Generic;

// Implements the BeeHive Construction Algorithm
public class Algorithm
{
    private Hashtable lookupTable;
    private int m;
    private int tMax;
    private List<Bee> bees;
    public BeeHiveGrid grid { get; set; }

    public Algorithm(int m, int tMax)
    {
        this.m = m;
        this.tMax = tMax;
        this.lookupTable = new Hashtable();
        this.bees = new List<Bee>();
    }

    void Start()
    {
        // TODO: place one block at top of grid

        for (int k = 1; k <= this.m; k++)
        {
            // TODO: randomly assign agent k to an unoccupied site
            this.bees.Add(new Bee(k, new Point3D()));
        }

        // Main loop
        for (int t = 1; t <= this.tMax; t++)
        {
            Update();
        }
    }

    void Update()
    {
        for (int k = 1; k <= this.m; k++)
        {
            // TODO: sense local conficuration (look at surrounding blocks)
            bool isInLookupTable = false;
            if (isInLookupTable)
            {
                // TODO: deposit brick specified by lookup table
                // TODO: draw a new brick
            }
            // TODO: move to random, unoccupied, adjacent site
            this.bees[k].position = new Point3D();
        }
    }

    void main(string[] args)
    {
        Algorithm a = new Algorithm(10, 100);
        a.Start();
    }
}
