using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIOClientApp
{
    class DummyData
    {
        public int id { get; set; }
        public string htmlData { get; set; }

        public override string ToString()
        {
            return $"id: {id}, data-len: {htmlData.Length}";
        }
    }
}
