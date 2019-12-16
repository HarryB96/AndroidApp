using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AndroidApp.Models
{
    [Table("Program")]
    class Program
    {
        [PrimaryKey,MaxLength(100)]
        public string ExerciseName { get; set; }
        public float Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        [MaxLength(20)]
        public string Day { get; set; }
        [MaxLength(10)]
        public string Superset { get; set; }
    }
}
