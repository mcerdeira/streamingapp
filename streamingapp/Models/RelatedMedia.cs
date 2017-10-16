using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace streamingapp.Models
{
    public class RelatedMedia
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string MediaLink { get; set; }
        [Required]
        public string Format { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
