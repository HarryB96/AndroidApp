using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AndroidApp.Models
{
    [Table("Program")]
    class Program
    {
        [PrimaryKey]
        public string ExerciseName { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public string Day { get; set; }
        public string Superset { get; set; }
    }
}
