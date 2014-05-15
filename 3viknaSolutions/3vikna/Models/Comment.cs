using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int subtitleID { get; set; }
        public String UserName { get; set; }
        public String CommentText { get; set; }
    }
}