using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stracker.Models;
using Stracker.ViewModel;

namespace Stracker.Controllers
{
    public class BusinessLayerController : Controller
    {
        private GolfStatTrackerEntities db = new GolfStatTrackerEntities();

        private static JsonSerializerSettings JsonSetting
        {
            get
            {
                var settings = new JsonSerializerSettings();

                settings.ContractResolver = new DefaultContractResolver()
                {
                    IgnoreSerializableAttribute = true,
                    IgnoreSerializableInterface = true
                };
                return settings;
            }
        }

        #region Facilities
        public List<Facility> GetFacilityEntities()
        {
            return db.Facilities.ToList();
        }

        public string GetFacilities()
        {
            var facilities = db.Facilities.ToList();
            var list = new List<Facility>();
            foreach (Facility f in facilities)
            {
                var facilityObj = new Facility();
                facilityObj.FacilityId = f.FacilityId;
                facilityObj.Name = f.Name;
                list.Add(facilityObj);
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented, JsonSetting);
        }


        #endregion

        #region Courses
        public string GetCourses(int facilityId)
        {
            var courses = db.Courses.Where(f => f.FacilityId == facilityId).ToList();
            var list = new List<Cours>();
            foreach (Cours c in courses)
            {
                var courseObj = new Cours();
                courseObj.CourseId = c.CourseId;
                courseObj.Name = c.Name;
                list.Add(courseObj);
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented, JsonSetting);
        }

        public List<Cours> GetCoursesEntitiesByFacility(int facilityId)
        {
            var courses = db.Courses.Where(f => f.FacilityId == facilityId).ToList();
            return courses;
        }

        #endregion

        #region Tees
        public string GetTees(int facilityId, int courseId)
        {
            var tees = db.Tees.Where(f => f.FacilityId == facilityId && f.CourseId == courseId).ToList();
            var list = new List<Tee>();
            foreach (Tee t in tees)
            {
                var teeObj = new Tee();
                teeObj.TeeId = t.TeeId;
                teeObj.Name = t.Name;
                list.Add(teeObj);
            }
            return JsonConvert.SerializeObject(list, Formatting.Indented, JsonSetting);
        }

        public List<Tee> GetTeesEntitiesByFacilityAndCourse(int facilityId, int courseId)
        {
            var tees = db.Tees.Where(f => f.FacilityId == facilityId && f.CourseId == courseId).ToList();
            return tees;
        }

        #endregion

        #region Rounds

        public string GetScorecardDetails(int? roundId)
        {
            var roundObj = new Round();
            roundObj = db.Rounds.First(x => x.RoundId == roundId);

            //var facility = new Facility();
            //facility = db.Facilities.FirstOrDefault(x => x.FacilityId == roundObj.FacilityId);

            //var course = new Cours();
            //course = db.Courses.FirstOrDefault(x => x.FacilityId == roundObj.FacilityId &&
                                                    //x.CourseId == roundObj.CourseId);

            var tees = db.Tees.Where(x => x.FacilityId == roundObj.FacilityId &&
                                         x.CourseId == roundObj.CourseId &&
                                         x.TeeId == roundObj.TeeId).ToList();

            //var holes = new List<Hole>();
            //holes = db.Holes.Where(x => x.FacilityId == roundObj.FacilityId &&
            //    x.CourseId == roundObj.CourseId &&
            //    x.TeeId == roundObj.TeeId).ToList();

            //Tuple<Facility, Cours, Tee, List<Hole>> cardDetail =
            //    new Tuple<Facility, Cours, Tee, List<Hole>>(facility, course, tee, holes);

            var json = JsonConvert.SerializeObject(tees, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return json;


        }

        public Scorecard GetScorecardDetailEntity(int? roundId)
        {
            var roundObj = new Round();
            roundObj = db.Rounds.First(x => x.RoundId == roundId);

            var facility = new Facility();
            facility = db.Facilities.FirstOrDefault(x => x.FacilityId == roundObj.FacilityId);

            var course = new Cours();
            course = db.Courses.FirstOrDefault(x => x.FacilityId == roundObj.FacilityId &&
            x.CourseId == roundObj.CourseId);

            var tees = db.Tees.Where(x => x.FacilityId == roundObj.FacilityId &&
                                         x.CourseId == roundObj.CourseId &&
                                         x.TeeId == roundObj.TeeId).ToList();

            var holes = new List<Hole>();
            holes = db.Holes.Where(x => x.FacilityId == roundObj.FacilityId &&
                x.CourseId == roundObj.CourseId &&
                x.TeeId == roundObj.TeeId).ToList();

            //Tuple<Facility, Cours, Tee, List<Hole>> cardDetail =
            //    new Tuple<Facility, Cours, Tee, List<Hole>>(facility, course, tee, holes);

            var scorecard = new Scorecard();
            scorecard.Round = roundObj;
            scorecard.Tees = tees;
            scorecard.Facility = facility;
            scorecard.Course = course;
            scorecard.Holes = holes;
            return scorecard;
        }

        #endregion

    }
}