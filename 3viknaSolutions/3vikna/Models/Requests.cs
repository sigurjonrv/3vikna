using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class Requests
    {
        public int ID { get; set; }
        public string MediaName { get; set; }
        public string YearPublished { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string File { get; set; }
        public int UpvoteID { get; set; }

        public Requests()
        {
            Date = DateTime.Now;
        }
    }
}