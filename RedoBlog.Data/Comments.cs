using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoBlog.Data
{
    public class Comments
    {
        public int Id { get; set; }
        public string cName { get; set; }
        public string ccomment { get; set; }
        public int cblogid { get; set; }
    }
}
