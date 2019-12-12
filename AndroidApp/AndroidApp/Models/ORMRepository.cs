using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AndroidApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace AndroidApp
{
    class ORMRepository
    {
        SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        //Constructor for creating the One Rep Max repository
        public ORMRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<OneRepMax>().Wait();
        }

        //Asynchronous task for adding  a new One Rep Max to the database
        public async Task AddNewORMAsync(string exercise, double oneRep, double weight, int reps)
        {
            int result = 0;
            try
            {
                //basic validation to ensure an exercise was entered
                if (string.IsNullOrEmpty(exercise))
                    throw new Exception("Valid exercise required");

                result = await conn.InsertAsync(new OneRepMax { ExerciseName = exercise , OneRep = oneRep, Reps = reps, Weight = weight});

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, exercise);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", exercise, ex.Message);
            }
        }

        //Asynchronous task to remove an entry form the database
        public async Task RemoveORMAsync(OneRepMax oneToDelete)
        {
            await conn.DeleteAsync(oneToDelete);
        }

        //Asynchronous task to get the information from the database and add it to a list of one rep maxes
        public async Task<List<OneRepMax>> GetOneRepMaxesAsync()
        {
            try
            {
                return await conn.Table<OneRepMax>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<OneRepMax>();
        }
    }
}
