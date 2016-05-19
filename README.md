# BeeHiveSim
##### _Authors:_ Garrett Barnes, Sean Carter, Bo Peng
##### Department of Computer Science and Software Engineering, Rose-Hulman Institute of Technology

##### Created in partial fulfillment of the requirements of the course CSSE453: Swarm Intelligence.

---

### Introduction
The BeeHiveSim project aims to reconstruct and expand upon the beehive construction algorithm and simulations of Guy Theraulaz and Eric Bonabeau in their research paper ["Modelling the Collective Building of Complex Architectures in Social Insects with Lattice Swarms"](http://www.eecs.harvard.edu/~rad/courses/cs266/papers/theraulaz-thbio1995.pdf).

The project uses a lattice swarm on a 3D hexagonal grid to simulate the construction of beehives.
See the [Google Slides presentation](https://docs.google.com/presentation/d/1jKdFDAl5R30eb7bLcXj4hGZUI1Mtgbu5J3WiFv5-t5M/edit?usp=sharing) for more information.

### Rights
This project is freeware. Feel free to use it in demonstrations or extend upon it, with proper credit to the authors. For extension ideas, see "Future Improvements" below.

### Tools
This project leverages C# to implement the algorithm, and the Unity3D engine to visualize the results of the simulation.

### Preparing and Running the Simulation

1. [Download and install](https://unity3d.com/get-unity)  the Unity engine, Personal Edition, with default settings. Word of caution: this is around ~9GB to download.

2. Download or fork the project.

3. Run Unity and open the project with it.

4. From the "Hierarchy" pane of Unity's default editor view, select the "Hexagon" element. You should see several text fields appear in the Inspector pane on the right side of the screen.

5. The fields under "Update Cells (Script)" are configurable parameters for the simulation. They are detailed below.

6. Once you have the parameters set to your liking (the default settings will give you a feel for the simulation), click the play button on the upper-middle part of the screen.

7. You can pan the camera forward, backward, left, and right with WASD, raise and lower it with the Up and Down arrows, and adjust angle by holding the left mouse button and dragging.

#### Parameters

| Parameter | Usage |
|-----------|-------|
| X | The number of cells in the simulation's X-axis |
| Y | The number of cells in the simulation's Y-axis |
| Z | The number of cells in the simulation's Z-axis |
| Init X | The X-value of the starting cell |
| Init Y | The Y-value of the starting cell |
| Init Z | The Z-value of the starting cell |
| Init Type | The type of brick that the starting cell will have (currently, 1 and 2 are supported) |
| N Bees | The number of bees to use in the simulation |
| N Steps | The number of steps to run the simulation |
| Path | Path to the ruleset file |

#### Notes on parameters

- Init {X, Y, Z} should be in the range [0, {X, Y, Z} - 1], respectively

- More bees means more work, so expect lag for more than 50-100

- The more cells are filled, the less efficient the update process becomes, so be wary of lag there (see "Future Improvements" if you'd like to help us fix this)

- The path can be relative or absolute. The Architectures we have implemented are in "Assets/Configs"; we encourage you to investigate them.

#### Architecture files
The architecture files are plaintext files, laid out as follows.

---

Given a 3x3x3 matrix with one matching configuration (as in the appendices of Theraulaz and Bonabeau's paper), the translation to one row of the file is:

| 11 12 13 |....| 21 22 23 |....| 31 32 33 |
| 14 15 16 |....| 24 25 26 |....| 34 35 36 |  ->  11,12,16,19,18,14,15 21,22,26,29,28,24 31,32,36,39,38,34,35 X Y
| 17 18 19 |....| 27 28 29 |....| 37 38 39 |
Where X is the brick type to place and Y is the decimal chance [0..1] to place the brick (though this is not yet used in the algorithm).

---

As you can see, elements 3 and 7 of each matrix are ignored; this is caused by the loss of those corners when skewing the Cartesian matrix into a hexagonal one. Element 25 is ignored because it is the Bee's current location, and thus will have no brick.

In addition, you can make a comment or comment out a particular rule with a "//" at the beginning of a line.
Currently, a "#" at the beginning of a line will also be ignored, but in the future we plan to use those lines to separate rulesets for multi-set architectures (such as Theraulaz and Bonabeau's Architecture 4n)

### Future Improvements
If you would like to improve or extend this simulation, here are a few of the things we wanted to do, given more time:

- A Genetic algorithm to mutate and recombine Local rules

- Varying the ruleset used based on which step of the construction you are in (Theraulaz and Bonabeau did this in their experiments, but did notgith detail how they implemented it)

- Smart bees that can match a partial configuration or increase their sensory range to match a full configuration for the next step, and move to the most promising adjacent location based on the best match

- Percentage chance of placing a brick. This is in place, but not yet used in the algorithm

- Improving rendering efficiency. The transformation and render step currently varies with the number of rendered cells, as all such cells are regenerated at each step. It would not be terribly difficult to correct it to a constant by storing which cells have changed in the algorithm's update step and only changing those cells in the rendering step.
