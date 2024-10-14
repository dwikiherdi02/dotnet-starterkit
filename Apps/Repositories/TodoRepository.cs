using Apps.Data;
using Apps.Entities;
using Apps.Interfaces.Repositories;
using Apps.Models;
using Microsoft.EntityFrameworkCore;

namespace Apps.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoCtx;
        
        public TodoRepository(TodoContext todoCtx)
        {
            _todoCtx = todoCtx;
        }

        public async Task<(IEnumerable<Todo> list, int count)> FindAll(TodoEntityQuery queryParams)
        {
            var query = _todoCtx.Todos.AsQueryable();
            
            if (queryParams.Search != null)
            {
                query = query.Where(q => q.Name.Contains(queryParams.Search));
            }

            // * Generate count before limiting
            var count = await query.CountAsync();

            if (queryParams.PageSize > 0)
            {
                if (queryParams.Page > 0)
                {
                    query = query.Skip((queryParams.Page - 1) * queryParams.PageSize);
                }

                query = query.Take(queryParams.PageSize);
            }

            // * Generate list of todo
            var list = await query.ToListAsync();

            return (list, count);
        }
        
        public async Task<Todo?> FindById(Guid id)
        {
            // return await _todoCtx.Todos.SingleAsync(q => q.Id == id);   
            return await _todoCtx.Todos.FindAsync(id);   
        }
        
        public async Task<Todo?> Store(Todo item)
        {
            using var transaction = _todoCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("StoreTodoItem");

                _todoCtx.Todos.Add(item);
                await _todoCtx.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("StoreTodoItem");

                return null;
            }

            return item;   
        }
        
        // public async Task<bool> Update()
        // {
        //     return true;   
        // }
        
        // public async Task<bool> Delete()
        // {
        //     return true;   
        // }
        
    }
}