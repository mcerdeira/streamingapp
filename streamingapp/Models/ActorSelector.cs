using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace streamingapp.Models
{
    /*
     Actor selector as a component
    */

    public class ActorSelector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}