using Less.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Transactions;
using Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Application.Test
{
    public class DatabaseFixture : IDisposable
    {
        ConfigurationManager Configuration = new ConfigurationManager();
        public DataBaseContext Context;
        private readonly TransactionScope _scope;
        private readonly UserTestsBuilder _builder;
        public DatabaseFixture()
        {
            _builder = new UserTestsBuilder();
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseSqlServer(Configuration["ConnectionStrings:SqlServer1"])
                .Options;
            Context = new DataBaseContext(options);
            _scope = new TransactionScope();
            var user = new IdentityUser("NAME", "LASTNAME");
            Context.Add(user);
        }

        public void Dispose()
        {
            _scope.Dispose();
            Context.Database.ExecuteSqlRaw("truncate table [DBNAME].[dbo].[TABLENAME]");
            Context.SaveChanges();
            Context.Dispose();
        }
    }
}
