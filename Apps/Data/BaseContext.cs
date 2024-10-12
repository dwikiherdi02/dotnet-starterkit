using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apps.Settings;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apps.Data
{
    public class BaseContext : DbContext
    {

        private readonly IOptions<Database> _database;

        public BaseContext(IOptions<Database> database) {
            _database = database;
        }

        // public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _database.Value.MySql;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}