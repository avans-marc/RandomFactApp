using RandomFactApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<ToDo>> GetToDosAsync();

        Task AddToDoAsync(ToDo todo);
    }
}