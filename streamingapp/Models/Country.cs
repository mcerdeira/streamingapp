using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace streamingapp.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Country")]
        public string Name { get; set; }
        public string Flaglink { get; set; }
    }
}
