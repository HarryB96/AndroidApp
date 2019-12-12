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

        public ORMRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<OneRepMax>().Wait();
        }

        public async Task AddNewORMAsync(string exercise)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(exercise))
                    throw new Exception("Valid exercise required");

                result = await conn.InsertAsync(new OneRepMax { ExerciseName = exercise });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, exercise);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", exercise, ex.Message);
            }
        }

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
