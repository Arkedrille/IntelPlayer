using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IntelPlayer
{
    [Serializable]//indique qu'une classe est sérialisable
    class Morceaux
    {
        public string type { get; set;  }
        public int number { get; set; }
        public bool Healthy { get; set; }

        public Morceaux(string t, int n, bool h)
        {
            this.type = t;
            this.number = n;
            this.Healthy = h;
        }
    }
}
