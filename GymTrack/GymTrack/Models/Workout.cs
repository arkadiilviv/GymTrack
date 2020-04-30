using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTrack.Models
{
    public class Workout
    {
        public int id { get; set; }
        public virtual Person Owner { get; set; }
        public double Weight { get; set; }
        public string Date { get; set; }
        public virtual Exercise Exercise_Workout { get; set; }
    }
}
