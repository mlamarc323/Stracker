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
    public class RoundDetailsController : Controller
    {
        private GolfStatTrackerEntities db = new GolfStatTrackerEntities();

        // GET: RoundDetails
        public ActionResult Index()
        {
            var roundDetails = db.RoundDetails.Include(r => r.Round);
            return View(roundDetails.ToList());
        }

        // GET: RoundDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundDetail roundDetail = db.RoundDetails.Find(id);
            if (roundDetail == null)
            {
                return HttpNotFound();
            }
            return View(roundDetail);
        }

        // GET: RoundDetails/Create
        public ActionResult Create()
        {
            ViewBag.RoundId = new SelectList(db.Rounds, "RoundId", "RoundId");
            return View();
        }

        // POST: RoundDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoundDetailId,RoundId,HoleId,Score,Putts,GIR,FIR")] RoundDetail roundDetail)
        {
            if (ModelState.IsValid)
            {
                db.RoundDetails.Add(roundDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoundId = new SelectList(db.Rounds, "RoundId", "RoundId", roundDetail.RoundId);
            return View(roundDetail);
        }

        // GET: RoundDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundDetail roundDetail = db.RoundDetails.Find(id);
            if (roundDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoundId = new SelectList(db.Rounds, "RoundId", "RoundId", roundDetail.RoundId);
            return View(roundDetail);
        }

        // POST: RoundDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoundDetailId,RoundId,HoleId,Score,Putts,GIR,FIR")] RoundDetail roundDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roundDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoundId = new SelectList(db.Rounds, "RoundId", "RoundId", roundDetail.RoundId);
            return View(roundDetail);
        }

        // GET: RoundDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundDetail roundDetail = db.RoundDetails.Find(id);
            if (roundDetail == null)
            {
                return HttpNotFound();
            }
            return View(roundDetail);
        }

        // POST: RoundDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoundDetail roundDetail = db.RoundDetails.Find(id);
            db.RoundDetails.Remove(roundDetail);
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
