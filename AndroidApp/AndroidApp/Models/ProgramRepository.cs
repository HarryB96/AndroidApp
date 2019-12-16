using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AndroidApp.Models;
using System.Threading.Tasks;

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
            if (conn.Table<Program>().FirstOrDefaultAsync() == null)
            {

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
    }
}