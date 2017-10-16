using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace streamingapp.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Director Name")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }
        public Country Nationality { get; set; }
    }
}
