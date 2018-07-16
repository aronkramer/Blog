using RedoBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedoBlog.Models
{
    public class BlogAndComments
    {
        public int Id { get; set; }
        public Blogs Blog { get; set; }
        public IEnumerable<Comments> comments { get; set; }
        public string CookieName{ get; set; }
    }
}