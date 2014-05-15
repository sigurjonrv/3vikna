using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class Upvote
    {
        public int ID { get; set; }

        public int ReqID { get; set; }

        public string UserName { get; set; }
    }
}