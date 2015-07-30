using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stracker.Models;

namespace Stracker.Controllers
{
    public class HolesController : Controller
    {
        private GolfStatTrackerEntities db = new GolfStatTrackerEntities();
        BusinessLayerController businessLayer = new BusinessLayerController();

        // GET: Holes
        public ActionResult Index()
        {
            var holes = db.Holes.Include(h => h.Tee);
            return View(holes.ToList());
        }

        // GET: Holes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Holes.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            return View(hole);
        }

        // GET: Holes/Create
        public ActionResult Create()
        {
            var facilities = businessLayer.GetFacilityEntities();
            ViewBag.FacilityId = new SelectList(facilities, "FacilityId", "Name");
            //ViewBag.TeeId = new SelectList(db.Tees, "TeeId", "Name");
            return View();
        }

        // POST: Holes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HoleId,TeeId,Number,Yardage,Par,Handicap,FacilityId,CourseId")] Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.Holes.Add(hole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeeId = new SelectList(db.Tees, "TeeId", "Name", hole.TeeId);
            return View(hole);
        }

        // GET: Holes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Holes.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeeId = new SelectList(db.Tees, "TeeId", "Name", hole.TeeId);
            return View(hole);
        }

        // POST: Holes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HoleId,TeeId,Number,Yardage,Par,Handicap,FacilityId,CourseId")] Hole hole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeeId = new SelectList(db.Tees, "TeeId", "Name", hole.TeeId);
            return View(hole);
        }

        // GET: Holes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hole hole = db.Holes.Find(id);
            if (hole == null)
            {
                return HttpNotFound();
            }
            return View(hole);
        }

        // POST: Holes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hole hole = db.Holes.Find(id);
            db.Holes.Remove(hole);
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
