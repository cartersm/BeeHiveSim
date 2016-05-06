using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Editor;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets.Algorithm
{
    public static class ConfigParser
    {
        public static Dictionary<LocalConfiguration, BrickPlacement> Parse(string filename)
        {
            Dictionary<LocalConfiguration, BrickPlacement> dict = new Dictionary<LocalConfiguration, BrickPlacement>();
            var text = filename;
            Cell[,] cells = new Cell[3,7];
            using (var streamReader = new StreamReader(text, Encoding.UTF8))
            {
                string line;
                while (streamReader.Peek() >= 0)
                {
                    line = streamReader.ReadLine();
                    string[] fields = line.Split(' ');
                    string[] top = fields[0].Split(',');
                    string[] mid = fields[1].Split(',');
                    string[] bot = fields[2].Split(',');
                    BrickPlacement brick = new BrickPlacement(Int32.Parse(fields[3]), Double.Parse(fields[4]));
                    for (int i = 0; i < 6; i++)
                    {
                        cells[0, i] = new Cell(Int32.Parse(bot[i]));
                        cells[1, i] = new Cell(Int32.Parse(mid[i]));
                        cells[2, i] = new Cell(Int32.Parse(bot[i]));
                    }
                    cells[0, 6] = new Cell(Int32.Parse(bot[6]));
                    cells[0, 6] = null;
                    cells[0, 6] = new Cell(Int32.Parse(top[6]));
                    LocalConfiguration config = new LocalConfiguration(cells);
                    dict.Add(config, brick);
                }
            }


            return dict;


        }

       
    }
}
