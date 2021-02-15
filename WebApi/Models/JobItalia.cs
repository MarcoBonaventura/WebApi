using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class JobItalia
    {
        public int JobID { get; set; }
        public int Lib { get; set; }
        public string Macro { get; set; }
        public string Suspended { get; set; }
        public string JobName { get; set; }
        public string Friday2X { get; set; }
        public string Descr { get; set; }
        public string Params { get; set; }
        public string JobPage { get; set; }
        public int Prty { get; set; }
    }
}
