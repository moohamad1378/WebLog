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
using Moq;

namespace Application.Test.CQRS.Queries
{
    public  class GetUserQueryTest
    {
        [Fact]
        public  void GetUsersQueryHandlerTest()
        {
            //Arrange
            var moq = MockHelpers.MockUserManager<IdentityUser>();


            GetUsersQueryHandler getUsersQueryHandler = new GetUsersQueryHandler(moq.Object);

            var option = new DbContextOptionsBuilder<DataBaseContext>()
                        .UseInMemoryDatabase("test")
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                        .Options;
            var database = new DataBaseContext(option);
            database.AddRange(
                new IdentityUser { UserName = "mamad", Id = "11422221" },
                new IdentityUser { UserName = "ali", Id = "08690" }
            );
            database.SaveChanges();
            GetUsersQuery getUsersQuery = new GetUsersQuery();
            CancellationToken cancellationToken=new CancellationToken();
            //Act
            var result = getUsersQueryHandler.Handle(getUsersQuery, cancellationToken);
            //Assert
            Assert.NotNull(result);
            
            database.Dispose();
        }
    }
}
