using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class MainPageModelView
    {
        public IEnumerable<_3vikna.Models.Requests> Req { get; set; }
        public IEnumerable<_3vikna.Models.Subtitles> Sub { get; set; }
    }
}