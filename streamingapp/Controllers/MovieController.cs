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
    public class MovieController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Movie
        public ActionResult Index()
        {
            var movie = db.Movie.Include(m => m.Director).Include(m => m.Media);
            return View(movie.ToList());
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            MovieVM moviesvm = new MovieVM();
            moviesvm.Id = id.Value;
            moviesvm.Title = movie.Title;
            moviesvm.DirectorId = movie.DirectorId;
            moviesvm.MediaId = movie.MediaId;
            moviesvm.Rank = movie.Rank;
            moviesvm.ReleaseDate = movie.ReleaseDate;

            List<ActorSelector> actorselectorvm = new List<ActorSelector>();

            var result = from a in db.Actor
                         where
                         (from m in db.MovieActors
                          where m.ActorId == a.Id && m.MovieId == id
                          select m.ActorId).Contains(a.Id)

                         select a;

            foreach (var item in result)
            {
                ActorSelector asel = new ActorSelector();
                asel.Id = item.Id;
                asel.Name = item.Name;
                actorselectorvm.Add(asel);
            }

            moviesvm.ActorSelector = actorselectorvm;

            return View(moviesvm);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            ViewBag.DirectorId = new SelectList(db.Director, "Id", "Name");
            ViewBag.MediaId = new SelectList(db.RelatedMedia, "Id", "MediaLink");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,MediaId,ReleaseDate,Producer,DirectorId,Rank")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectorId = new SelectList(db.Director, "Id", "Name", movie.DirectorId);
            ViewBag.MediaId = new SelectList(db.RelatedMedia, "Id", "MediaLink", movie.MediaId);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorId = new SelectList(db.Director, "Id", "Name", movie.DirectorId);
            ViewBag.MediaId = new SelectList(db.RelatedMedia, "Id", "MediaLink", movie.MediaId);

            MovieVM moviesvm = new MovieVM();
            moviesvm.Id = id.Value;
            moviesvm.Title = movie.Title;
            moviesvm.DirectorId = movie.DirectorId;
            moviesvm.MediaId = movie.MediaId;
            moviesvm.Rank = movie.Rank;
            moviesvm.ReleaseDate = movie.ReleaseDate;
            
            List<ActorSelector> actorselectorvm = new List<ActorSelector>();

            var result = from a in db.Actor
                         select new
                         {
                             a.Id,
                             a.Name,
                             Selected = ((from m in db.MovieActors
                                         where m.ActorId == a.Id && m.MovieId == id
                                         select m).Count() > 0)
                         };

            foreach (var item in result)
            {
                ActorSelector asel = new ActorSelector();
                asel.Id = item.Id;
                asel.Name = item.Name;
                asel.Selected = item.Selected;
                asel.Name = asel.Name;
                actorselectorvm.Add(asel);
            }

            moviesvm.ActorSelector = actorselectorvm;
            return View(moviesvm);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieVM movie)
        {
            if (ModelState.IsValid)
            {
                // borrar MovieActors where MovieId == movie.Id
                var query = from m in db.MovieActors
                            where m.MovieId == movie.Id
                            select m;

                foreach (var item in query)
                {
                    db.Entry(item).State = EntityState.Deleted;
                }

                foreach (var item in movie.ActorSelector)
                {
                    if (item.Selected)
                    {
                        db.MovieActors.Add(new MovieActors() { ActorId = item.Id, MovieId = movie.Id });
                    }
                }
                //db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorId = new SelectList(db.Director, "Id", "Name", movie.DirectorId);
            ViewBag.MediaId = new SelectList(db.RelatedMedia, "Id", "MediaLink", movie.MediaId);
            return View(movie);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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
