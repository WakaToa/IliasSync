using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IliasSync.Ilias
{
    public class Container
    {
        public string Name { get; set; }
        public int Id { get; set; }

        [JsonIgnore]
        public List<Container> Containers { get; set; }

        public Container()
        {
            Containers = new List<Container>();
        }

        public Container(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
