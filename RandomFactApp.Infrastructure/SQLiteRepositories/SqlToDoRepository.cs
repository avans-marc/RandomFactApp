using Microsoft.VisualBasic;
using RandomFactApp.Domain.Models;
using RandomFactApp.Domain.Repositories;
using RandomFactApp.Infrastructure.SQLiteRepositories.Entities;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Infrastructure.SQLiteRepositories
{
    public class SqlToDoRepository : ITodoRepository
    {
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        private readonly SQLiteAsyncConnection db;

        public SqlToDoRepository(string databasePath)
        {
            db = new SQLiteAsyncConnection(databasePath, Flags);
        }

        public async Task AddToDoAsync(ToDo todo)
        {
            await Init();

            await db.InsertAsync(new ToDoEntity { Label = todo.Label });
        }

        public async Task<IEnumerable<ToDo>> GetToDosAsync()
        {
            await Init();

            var query = db.Table<ToDoEntity>();
            var entities = await query.ToListAsync();

            // Map entity to domain model (you could use Automapper or Mapperly for this)
            // In some environments it is considered best practise to have separate objects for entities and domain models
            // to keep the domain really persistence ignorant (the domain does not care where it is persisted). 
            return entities.Select(x => new ToDo {  Label = x.Label });
        }

        private async Task Init()
        {
            var result = await db.CreateTableAsync<ToDoEntity>();
        }
    }
}