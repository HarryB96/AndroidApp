using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AndroidApp.Models;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;

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
            if (conn.Table<Program>().CountAsync().Result == 0)
            {
                var list = GetJsonDataAsync().Result;
                foreach (var item in list)
                {
                    conn.InsertAsync(item);
                }
            }
        }
        //Retrieving program data from the SQLite database
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
        //Retrieving initial data for the program page from a JSON file
        public async Task<List<Program>> GetJsonDataAsync()
        {
            string jsonFileName = "ProgramContent.json";
            List<Program> programFromJson = new List<Program>();
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                var jsonString = await reader.ReadToEndAsync();
                programFromJson = JsonConvert.DeserializeObject<List<Program>>(jsonString);
            }
            return programFromJson;
        }
    }
}