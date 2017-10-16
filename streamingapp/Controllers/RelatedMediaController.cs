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
    public class RelatedMediaController : Controller
    {
        private DataContext db = new DataContext();

        // GET: RelatedMedia
        public ActionResult Index()
        {
            return View(db.RelatedMedia.ToList());
        }

        // GET: RelatedMedia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelatedMedia relatedMedia = db.RelatedMedia.Find(id);
            if (relatedMedia == null)
            {
                return HttpNotFound();
            }
            return View(relatedMedia);
        }

        // GET: RelatedMedia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RelatedMedia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MediaLink,Format,Duration")] RelatedMedia relatedMedia)
        {
            if (ModelState.IsValid)
            {
                db.RelatedMedia.Add(relatedMedia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relatedMedia);
        }

        // GET: RelatedMedia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelatedMedia relatedMedia = db.RelatedMedia.Find(id);
            if (relatedMedia == null)
            {
                return HttpNotFound();
            }
            return View(relatedMedia);
        }

        // POST: RelatedMedia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MediaLink,Format,Duration")] RelatedMedia relatedMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relatedMedia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relatedMedia);
        }

        // GET: RelatedMedia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelatedMedia relatedMedia = db.RelatedMedia.Find(id);
            if (relatedMedia == null)
            {
                return HttpNotFound();
            }
            return View(relatedMedia);
        }

        // POST: RelatedMedia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RelatedMedia relatedMedia = db.RelatedMedia.Find(id);
            db.RelatedMedia.Remove(relatedMedia);
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
