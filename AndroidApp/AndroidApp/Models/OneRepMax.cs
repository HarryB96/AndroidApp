using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AndroidApp.Models
{
    [Table("OneRepMaxes")]
    class OneRepMax
    {
        [PrimaryKey]
        public string ExerciseName { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double OneRep { get; set; }
    }
}
