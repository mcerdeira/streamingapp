﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace streamingapp.Models
{
    public class MovieBrowserVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int MediaId { get; set; }
        public virtual RelatedMedia Media { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Producer { get; set; }
        public int DirectorId { get; set; }
        public string Rank { get; set; }
        public virtual Director Director { get; set; }
        public virtual List<Actor> Actor { get; set; }
    }
}