using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMonitorServer
{
    class ExplorerTreeElement
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public bool IsFolder { get; set; }

        public bool IsRoot { get; set; } = false;

        public override string ToString()
        {
            return IsFolder ? System.IO.Path.GetFileName(Name) + "\\" : System.IO.Path.GetFileName(Name);
        }
    }
}
