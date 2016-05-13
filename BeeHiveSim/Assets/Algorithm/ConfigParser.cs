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
            var dict = new Dictionary<LocalConfiguration, BrickPlacement>();
            var text = filename;
            using (var streamReader = new StreamReader(text, Encoding.UTF8))
            {
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    if (line == null) continue;
                    if (line.StartsWith("//")) continue;
                    var fields = line.Split(' ');
                    var top = fields[0].Split(',');
                    var mid = fields[1].Split(',');
                    var bot = fields[2].Split(',');
                    var brick = new BrickPlacement(int.Parse(fields[3]), double.Parse(fields[4]));
                    for (var j = 0; j < 6; j++)
                    {
                        var cells = new Cell[3, 7];
                        for (var i = 0; i < 6; i++)
                        {
                            var idx = (i + j) % 6;
                            cells[0, i] = new Cell(int.Parse(bot[idx]));
                            cells[1, i] = new Cell(int.Parse(mid[idx]));
                            cells[2, i] = new Cell(int.Parse(top[idx]));
                        }
                        cells[0, 6] = new Cell(int.Parse(bot[6]));
                        cells[1, 6] = null;
                        cells[2, 6] = new Cell(int.Parse(top[6]));

                        var config = new LocalConfiguration(cells);
                        try
                        {
                            dict.Add(config, brick);
                        }
                        catch (ArgumentException)
                        {
                            Debug.LogWarning("Found Duplicate Config");
                        }
                    }
                }
            }
            return dict;
        }
    }
}
