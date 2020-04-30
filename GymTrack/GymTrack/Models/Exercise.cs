using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTrack.Models
{
    public class Exercise
    {
        public int id { get; set; }
        public Person Owner { get; set; }
        public string ExerciseName { get; set; }
    }
}
