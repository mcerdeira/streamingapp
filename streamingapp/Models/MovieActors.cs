using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace streamingapp.Models
{
    /*
       Relationship between movies and actors
    */

    public class MovieActors
    {
        public int MovieActorsId { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}