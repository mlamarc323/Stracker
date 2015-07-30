using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stracker.Models;

namespace Stracker.ViewModel
{
    public class Scorecard
    {
        public Round Round { get; set; }
        public Facility Facility { get; set; }
        public Cours Course { get; set; }
        public Tee Tee { get; set; }
        public List<RoundDetail> Details { get; set; }
        public List<Hole> Holes { get; set; }
        public List<Tee> Tees { get; set; }
    }
}