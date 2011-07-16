using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permissions
{
    public class GroupPermissions
    {
        public string Name;
        public string Prefix;
        public float ColorR;
        public float ColorG;
        public float ColorB;
        public List<string> Nodes = new List<string>();
        public GroupPermissions(string name, string prefix, float colorR, float colorG, float colorB, List<string> nodes)
        {
            Name = name;
            Prefix = prefix;
            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
            Nodes = nodes;
        }
    }
}
