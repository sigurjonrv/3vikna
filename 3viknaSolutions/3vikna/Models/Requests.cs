using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class Requests
    {
        public int ID { get; set; }

        [Required (ErrorMessage = "Það þarf að skrifa inn heiti sjónvarpsefnis")]
        public string MediaName { get; set; }
        [Required(ErrorMessage = "Það þarf að skrifa inn útgáfuár")]
        public string YearPublished { get; set; }
        [Required(ErrorMessage = "Það þarf að vera dagsetning")]
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string File { get; set; }
        public int UpvoteID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageBytes { get; set; }
        public string Extension { get; set; }

        public Requests()
        {
            Date = DateTime.Now;
        }
    }
}