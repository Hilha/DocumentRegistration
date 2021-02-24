using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DocumentRegistration.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public CategoryModel Category { get; set; }

        [Required]
        public string Process { get; set; }
    }
}
