using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Editor;
using Newtonsoft.Json.Linq;

namespace Assets.Algorithm
{
    public static class ConfigParser
    {
        public static Dictionary<LocalConfiguration, BrickPlacement> Parse(string filename)
        {
            var text = filename;
            using (var streamReader = new StreamReader(text, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            var obj = JObject.Parse(text);

            var a = (JArray)obj["array"];
            var combos = obj.ToObject<IList<Combo>>();
            return combos.ToDictionary(entry => entry.Config, entry => new BrickPlacement(entry.BrickType, entry.Chance));
        }

        public class Combo
        {
            public LocalConfiguration Config { get; set; }
            public double Chance { get; set; }
            public int BrickType { get; set; }
        }
    }
}
