using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;


namespace Assets.Editor
{
    public class ConfigParser
    {
        

        Dictionary<LocalConfiguration, BrickPlacement> runParse(String filename)
        {
            Dictionary<LocalConfiguration, BrickPlacement> vals = new Dictionary<LocalConfiguration, BrickPlacement>();
              string text = filename;
            using (var streamReader = new StreamReader(text, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            JObject obj = JObject.Parse(text);

            JArray a = (JArray) obj["array"];
            IList<Combo> combos = obj.ToObject<IList<Combo>>();
            foreach (Combo entry in combos)
            {
                vals.Add(entry.config, new BrickPlacement(entry.bricktype,entry.prob) );
            }
            return vals;
        }
        
        public class Combo
        {
            public LocalConfiguration config { get; set; }
            public double prob { get; set; }
            public int bricktype { get; set; }


        }

    }

    
}
