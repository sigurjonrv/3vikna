using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace _3vikna.Models
{
    public class ValidateFile : ValidationAttribute
    {
        public ValidationResult IsValid( HttpPostedFileBase file)
        {
            if (file == null)
            {
                return new ValidationResult("Please upload a file!");
            }

            if (file.ContentLength > 10 * 1024 * 1024)
            {
                return new ValidationResult("This file is too big!");
            }
            
            string ext = Path.GetExtension(file.FileName);
            if (String.IsNullOrEmpty(ext) ||
               !ext.Equals(".srt", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("This file is not a PDF!");
            }
            return ValidationResult.Success;
        }

        public ValidateFile(HttpPostedFileBase file)
        {
            
        }
    }
}