using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AndroidApp.Models;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AndroidApp
{
    class ProgramRepository
    {
        SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }
        
        //Constructor for creating the Program repository
        public ProgramRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Program>().Wait();
            foreach (var item in GetDataFromFile().Result)
            {
                conn.InsertAsync(item);
            }
        }
        public async Task<List<Program>> GetProgramAsync()
        {
            try
            {
                return await conn.Table<Program>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Program>();
        }
        public async Task<List<Program>> GetDataFromFile()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Harry Barnett\Documents\ProgramContent.csv");
            string data = await sr.ReadToEndAsync();
            string[] dataArray = data.Split();
            List<Program> InitialProgram = new List<Program>();
            foreach (var item in dataArray)
            {
                string[] itemBreakdown = item.Split(',');
                InitialProgram.Add(new Program()
                {
                    ExerciseName = itemBreakdown[0],
                    Sets = int.Parse(itemBreakdown[1]),
                    Reps = int.Parse(itemBreakdown[2]),
                    Weight = double.Parse(itemBreakdown[3]),
                    Superset = itemBreakdown[4],
                    Day = itemBreakdown[5]
                });
            }
            return InitialProgram;
        }
    }
}