using Apps.Data.Ctx;
using Apps.Data.Entities;
using Apps.Data.Models;
using Apps.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apps.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _dbCtx;
        
        public TodoRepository(AppDbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public async Task<(IEnumerable<Todo> list, int count)> FindAll(TodoEntityQuery queryParams)
        {
            var query = _dbCtx.Todos.AsQueryable();
            
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

            query = query.OrderByColumn("CreatedAt", "desc");

            // Console.WriteLine(query.ToQueryString());
            // Console.WriteLine("--------------------------------------------------");

            // * Generate list of todo
            var list = await query.ToListAsync();

            return (list, count);
        }
        
        public async Task<Todo?> FindById(Ulid id)
        {
            // https://chatgpt.com/share/6724a786-2c98-8008-be0d-abd11fb73fab
            // return await _dbCtx.Todos.SingleOrDefaultAsync(q => q.Id == id);   
            // return await _dbCtx.Todos.FirstOrDefaultAsync(q => q.Id == id);
            try
            {
                return await _dbCtx.Todos.FindAsync(id.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public async Task<Todo?> Store(Todo item)
        {
            using var transaction = _dbCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("StoreTodoItem");

                _dbCtx.Todos.Add(item);
                await _dbCtx.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("StoreTodoItem");

                return null;
            }

            return item;
        }
        
        public async Task<bool> Update(Todo item)
        {
            using var transaction = _dbCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateTodoItem");

                item.UpdatedAt = DateTime.UtcNow;

                _dbCtx.Entry(item).State = EntityState.Modified;

                await _dbCtx.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateTodoItem");

                return false;
            }
            return true;
        }
        
        public async Task<bool> Destroy(Todo item)
        {
            using var transaction = _dbCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("DestroyTodoItem");

                _dbCtx.Todos.Remove(item);
                await _dbCtx.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("DestroyTodoItem");
                return false;
            }

            return true;
        }
        
    }
}