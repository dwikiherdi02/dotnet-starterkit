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
    }
}