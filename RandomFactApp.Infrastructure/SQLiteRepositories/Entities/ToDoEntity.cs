﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Infrastructure.SQLiteRepositories.Entities
{
    public class ToDoEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Label { get; set; }
    }
}