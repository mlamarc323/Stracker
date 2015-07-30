using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Stracker.Models;
using Stracker.ViewModel;

namespace Stracker.Controllers
{
    public class RoundsController : Controller
    {
        private GolfStatTrackerEntities db = new GolfStatTrackerEntities();
        private BusinessLayerController businessLayer = new BusinessLayerController();

        // GET: Rounds
        public ActionResult Index()
        {
            return View(db.Rounds.ToList());
        }

        // GET: Rounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Round round = db.Rounds.Find(id);
            if (round == null)
            {
                return HttpNotFound();
            }
            return View(round);
        }

        // GET: Rounds/Create
        public ActionResult Create()
        {
            var facilities = businessLayer.GetFacilityEntities();
            ViewBag.FacilityId = new SelectList(facilities, "FacilityId", "Name");
            return View();
        }

        // POST: Rounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoundId,FacilityId,CourseId,TeeId,Date,TotalScore")] Round round)
        {
            if (ModelState.IsValid)
            {
                round.Date = Convert.ToDateTime(round.Date.ToShortDateString());
                db.Rounds.Add(round);
                db.SaveChanges();
                return RedirectToAction("Scorecard", "Rounds", new { roundId = round.RoundId });
            }

            return View(round);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostScorecard([Bind(Exclude = "RoundDetailId")] Scorecard scorecard)
        {
            if (ModelState.IsValid)
            {
                int totalScore = 0;
                for (int i = 0; i < scorecard.Details.Count; i++)
                {
                    var detail = new RoundDetail();
                    detail.RoundDetailId = db.RoundDetails.Max(x => x.RoundDetailId) + 1;
                    detail.RoundId = scorecard.Round.RoundId;
                    detail.HoleId = scorecard.Holes[i].HoleId;
                    detail.Score = scorecard.Details[i].Score;
                    detail.Putts = scorecard.Details[i].Putts;
                    detail.GIR = scorecard.Details[i].GIR;
                    detail.FIR = scorecard.Details[i].FIR;
                    totalScore += Convert.ToInt32(detail.Score);
                    db.RoundDetails.Add(detail);
                    db.SaveChanges();
                }

                Round round = db.Rounds.Find(scorecard.Round.RoundId);
                round.RoundId = round.RoundId;
                round.TotalScore = totalScore;
                db.SaveChanges();
                
                return RedirectToAction("Index", "RoundDetails");
            }

            return View(scorecard);
        }



        // GET: Rounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Round round = db.Rounds.Find(id);
            if (round == null)
            {
                return HttpNotFound();
            }
            return View(round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoundId,FacilityId,CourseId,TeeId,Date,TotalScore")] Round round)
        {
            if (ModelState.IsValid)
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(round);
        }

        // GET: Rounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Round round = db.Rounds.Find(id);
            if (round == null)
            {
                return HttpNotFound();
            }
            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Round round = db.Rounds.Find(id);
            db.Rounds.Remove(round);
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

        // GET: Rounds/Scorecard
        public ActionResult Scorecard(int? roundId)
        {
            //var facilities = businessLayer.GetFacilityEntities();
            //ViewBag.FacilityId = new SelectList(facilities, "FacilityId", "Name");
            if (roundId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scorecard scorecard = new Scorecard();
            scorecard.Round = db.Rounds.Find(roundId);

            //Create dropdowns
            var gir = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "--",
                    Value = null
                },
                new SelectListItem
                {
                    Text = "Hit",
                    Value = "true"
                },
                new SelectListItem
                {
                    Text = "Miss",
                    Value = "false"
                }
            };

            var fir = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "--",
                    Value = null
                },
                new SelectListItem
                {
                    Text = "Hit",
                    Value = "true"
                },
                new SelectListItem
                {
                    Text = "Miss",
                    Value = "false"
                }
            };

            ViewBag.gir = gir;
            ViewBag.fir = fir;
            
            var scorecardDetails = businessLayer.GetScorecardDetailEntity(roundId);
            if (scorecardDetails == null)
            {
                return HttpNotFound();
            }
            return View(scorecardDetails);
            
        }

        // GET: Rounds/Edit/5
        public ActionResult EditRoundWithDetails(int? roundId)
        {
            if (roundId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Round round = db.Rounds.Find(roundId);
            if (round == null)
            {
                return HttpNotFound();
            }
            return View(round);
        }
    }
}
