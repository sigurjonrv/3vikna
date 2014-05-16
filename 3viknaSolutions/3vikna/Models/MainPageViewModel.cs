using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class MainPageViewModel
    {
        public IEnumerable<_3vikna.Models.Requests> popularRequests { get; set; }
        public IEnumerable<_3vikna.Models.Subtitles> newestSubtitles { get; set; }
        public IEnumerable<_3vikna.Models.Subtitles> notFinishedSubtitles { get; set; }
    }
}