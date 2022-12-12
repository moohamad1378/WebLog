using Application.CQRS.UsersCQRS.Queries;
using Application.Test.CQRS.Commans;
using EndPoint.Controllers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.CQRS.Queries
{
    public class GetUserQuery_Test
    {
        [Fact]
        public void GetUserQueryHandler_Test()
        {
            #region Arrange
            var moqUserManager = MockHelpers.MockUserManager<IdentityUser>().Object;
            //var moqImediator = new Mock<IMediator>();
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase("Test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var databas = new DataBaseContext(options);
            var moqgetUserQueryHandler = new Mock<GetUserQueryHandler>(moqUserManager
                , databas);
            databas.AddRange(
                new IdentityUser { UserName = "Ali", Id = "1234qwer" },
                new IdentityUser { UserName = "mamad", Id = "5678qwer" }
                );
            databas.SaveChanges();
            GetUserQueryHandlerDto getUserQueryHandlerDto = new GetUserQueryHandlerDto
            {
                UserId = "1234qwer",
            };
            GetUserQuery getUserQuery = new GetUserQuery(getUserQueryHandlerDto);
            CancellationToken cancellationToken = new CancellationToken();
            GetUserQueryRespons getUserQueryRespons = new GetUserQueryRespons
            {
                ProfileImageSrc = "1112314114",
                UserId = "1234qwer",
                UserName = "Ali"
            };
            #endregion

            #region Act

            //var result = moqImediator.Setup(p => p.Send(getUserQuery, cancellationToken).Result)
            //    .Returns(getUserQueryRespons);
            //var resilt1 = MoquserController.Setup(p => p.Edit(getUserQueryHandlerDto).Result);
            var result=moqgetUserQueryHandler.Setup(p=>p.Handle(getUserQuery, cancellationToken).Result).Returns(getUserQueryRespons);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<GetUserQueryRespons>(result);
            #endregion
            databas.Dispose();
        }
    }
}
