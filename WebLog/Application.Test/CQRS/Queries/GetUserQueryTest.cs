using Application.CQRS.UsersCQRS.Queries;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Application.Test.CQRS.Queries
{
    internal class GetUserQueryTest
    {
        public void GetUsersQueryHandlerTest()
        {
            GetUsersQueryHandler getUsersQueryHandler = new GetUsersQueryHandler();
            var option = new DbContextOptionsBuilder<DataBaseContext>()
                        .UseInMemoryDatabase("test")
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                        .Options;
            var database = new DataBaseContext(option);
            database.AddRange(
                new IdentityUser { UserName = "mamad", Id = "11422221" },
                new IdentityUser { UserName = "ali", Id = "08690" }
            );
            getUsersQueryHandler.Handle();
            database.SaveChanges();
            database.Dispose();
        }
    }
}
