using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using streamingapp.DAL;
using streamingapp.Models;

namespace streamingapp.Controllers
{
    public class MovieBrowserVMController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index(string SearchString)
        {
            List<MovieBrowserVM> mv = new List<MovieBrowserVM>();

            if (SearchString != null && SearchString != "")
            {

                var query = from m in db.Movie
                            join ma in db.MovieActors on m.Id equals ma.MovieId
                            join a in db.Actor on ma.ActorId equals a.Id
                            where m.Title.Contains(SearchString)
                            select new
                            {
                                m.Id,
                                m.Title,
                                m.MediaId,
                                m.Media,
                                m.ReleaseDate,
                                m.Producer,
                                m.DirectorId,
                                m.Director,
                                m.Rank      
                            };

                foreach (var item in query)
                {
                    mv.Add(new MovieBrowserVM
                    {
                        Id = item.Id,
                        Title = item.Title,
                        MediaId = item.MediaId,
                        Media = item.Media,
                        ReleaseDate = item.ReleaseDate,
                        Producer = item.Producer,
                        DirectorId = item.DirectorId,
                        Director = item.Director,
                        Rank = string.Join("", Enumerable.Repeat("&#9733;", item.Rank)) + string.Join("", Enumerable.Repeat("&#9734;", 5 - item.Rank))
                    });
                }       
            }
            return View(mv);
        }

        public ActionResult Watch(int? id)
        {
            WatchMovieVM wm = new WatchMovieVM();

            var result = from m in db.Movie
                         join r in db.RelatedMedia on m.MediaId equals r.Id
                         where r.Id == id
                         select new
                         {
                             m.Title,
                             r.MediaLink
                         };

            foreach(var item in result)
            {
                wm.MediaLink = item.MediaLink;
                wm.Title = item.Title;
            }
            return View(wm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
