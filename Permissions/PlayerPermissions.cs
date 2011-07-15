using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permissions
{

    public class PlayerPermissions
    {
        public string Name;
        public List<string> Nodes = new List<string>();
        public string Group = "";
        private string Prefix = "";

        public PlayerPermissions(string name, string group, string prefix, List<string> nodes)
        {
            Name = name;
            Nodes = nodes;
            Group = group;
            Prefix = prefix;
        }
        public bool HasPermission(string node)
        {
            string[] split = node.Split('.');
            foreach (string[] splitStr in Nodes.Select(str => str.Split('.')))
            {
                for (int i = 0; i < split.Length; i++)
                {
                    if (i >= splitStr.Length) continue;
                    if (split[i] == splitStr[i])
                    {
                        if (i == split.Length - 1 && split.Length == splitStr.Length) return true;
                        continue;
                    }
                    if (splitStr[i] == "*") return true;
                }
            }
            if (String.IsNullOrEmpty(Group)) return false;
            foreach (string[] splitStr in PermissionManager.Groups.Find(item => item.Name == Group).Nodes.Select(str => str.Split('.')))
            {
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i] == splitStr[i])
                    {
                        if (i == split.Length - 1 && split.Length == splitStr.Length) return true;
                        continue;
                    }
                    if (splitStr[i] == "*") return true;
                }
            }
            return false;
        }
        public string GetPrefix()
        {
            if (!String.IsNullOrEmpty(Prefix)) return Prefix;
            return !String.IsNullOrEmpty(Group) ? PermissionManager.Groups.Find(item => item.Name == Group).Prefix : "";
        }
        public float[] GetColor()
        {
            var ret = new float[3];
            if (String.IsNullOrEmpty(Group))
            {
                ret[0] = 255f;
                ret[1] = 240f;
                ret[2] = 20f;
            }
            else
            {
                ret[0] = PermissionManager.Groups.Find(item => item.Name == Group).ColorR;
                ret[1] = PermissionManager.Groups.Find(item => item.Name == Group).ColorG;
                ret[2] = PermissionManager.Groups.Find(item => item.Name == Group).ColorB;
            }
            return ret;
        }
    }
}
