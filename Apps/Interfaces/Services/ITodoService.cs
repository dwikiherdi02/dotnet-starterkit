using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apps.Entities;
using Apps.Models;

namespace Apps.Interfaces.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntityResponse>> FindAll(TodoEntityQuery queryParams);
        Task<TodoEntityResponse?> FindById(Guid id);
        Task<TodoEntityResponse?> Store(TodoEntityBody body);
        Task<bool?> Update(Guid id, TodoEntityBody body);
    }
}