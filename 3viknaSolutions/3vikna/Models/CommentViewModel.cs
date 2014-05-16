using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class CommentViewModel
    {
        public IEnumerable<_3vikna.Models.Comment> Com { get; set; }
        public int subtitleId { get; set; }
        public string MediaName { get; set; }
    }
}