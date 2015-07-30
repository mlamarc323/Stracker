using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Stracker.Models;

namespace Stracker.Controllers
{
    public class TeesController : Controller
    {
        private GolfStatTrackerEntities db = new GolfStatTrackerEntities();
        BusinessLayerController businessLayer = new BusinessLayerController();

        // GET: Tees
        public ActionResult Index()
        {
            return View(db.Tees.ToList());
        }

        // GET: Tees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tee tee = db.Tees.Find(id);
            if (tee == null)
            {
                return HttpNotFound();
            }
            return View(tee);
        }

        // GET: Tees/Create
        public ActionResult Create()
        {
            var facilities = businessLayer.GetFacilityEntities();
            ViewBag.FacilityId = new SelectList(facilities, "FacilityId", "Name");
            //ViewBag.CourseId = new SelectList(db.Courses.Where(x=>x.FacilityId == ViewBag.FacilityId), "CourseId", "Name");
            return View();
        }

        // POST: Tees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeeId,FacilityId,CourseId,Name,Slope,Rating,Yardage")] Tee tee)
        {
            if (ModelState.IsValid)
            {
                db.Tees.Add(tee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tee);
        }

        // GET: Tees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tee tee = db.Tees.Find(id);
            if (tee == null)
            {
                return HttpNotFound();
            }
            return View(tee);
        }

        // POST: Tees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeeId,CourseId,Name,Slope,Rating,Yardage")] Tee tee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tee);
        }

        // GET: Tees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tee tee = db.Tees.Find(id);
            if (tee == null)
            {
                return HttpNotFound();
            }
            return View(tee);
        }

        // POST: Tees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tee tee = db.Tees.Find(id);
            db.Tees.Remove(tee);
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
