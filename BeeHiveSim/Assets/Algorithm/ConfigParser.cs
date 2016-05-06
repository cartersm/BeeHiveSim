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
            using (var streamReader = new StreamReader(text, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            var obj = JObject.Parse(text);

            var a = (JArray)obj["array"];
            var combos = obj.ToObject<IList<Combo>>();

            var cells = new Cell[3][];
            foreach (var ce in combos)
            {
                cells[0] = ce.bot;
                cells[1] = ce.mid;
                cells[2] = ce.top;
                LocalConfiguration configmap = new LocalConfiguration(cells);
                BrickPlacement bric = new BrickPlacement(ce.BrickType,ce.Chance);
                dict.Add(configmap,bric);

            }
            return dict;


        }

        public class Combo
        {
            public Cell[] top { get; set; }
            public Cell[] mid { get; set; }
            public Cell[] bot { get; set; }
            public double Chance { get; set; }
            public int BrickType { get; set; }
        }
    }
}
