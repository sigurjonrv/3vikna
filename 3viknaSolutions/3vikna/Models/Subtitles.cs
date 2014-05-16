using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3vikna.Models
{
    public class Subtitles
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Það þarf að skrifa inn heiti sjónvarpsefnis")]
        public string MediaNameSub { get; set; }
        [Required(ErrorMessage = "Það þarf að skrifa inn útgáfuár")]
        public string YearPublished { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Það þarf að vera dagsetning")]
        public DateTime DateCreated { get; set; }
        public string File { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageBytes { get; set; }
        public string Extension { get; set; }
        public bool IsFinished { get; set; }

        public Subtitles()
        {
            DateCreated = DateTime.Now;
        }

    }
}