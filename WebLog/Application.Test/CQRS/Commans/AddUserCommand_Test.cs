using Application.CQRS.UsersCQRS.Commands;
using EndPoint.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.CQRS.Commans
{
    public class AddUserCommand_Test
    {
        [Fact]
        public  void Test_AddUserHandler()
        {
            #region Arrang
            var mochUsermanager = MockHelpers.MockUserManager<IdentityUser>();
            var signInManagerMock = new Mock<FakeSignInManager>();
            var moq = new Mock<IMediator>();
            AddUserCommandRequestDto userCommandRequestDto = new AddUserCommandRequestDto()
            {
                Email = "qwfvwnsvjnsvkns@gmai.com",
                Password = "aaaaaaad2dD@",
                UserName = "Mamadalikhoob13"
            };
            AddUserCommandResponse addUserCommandResponse = new AddUserCommandResponse()
            {
                Errors = null,
                UserId = "12345678910"
            };
            CancellationToken cancellationToken = new CancellationToken();
            AddUserCommand addUserCommand1 = new AddUserCommand(userCommandRequestDto);
            #endregion

            var resul = moq.Setup(p => p.Send(addUserCommand1, cancellationToken).Result).Returns(addUserCommandResponse);
            UserController userController = new UserController(mochUsermanager.Object, signInManagerMock.Object, moq.Object);
            
            #region Assert
            Assert.NotNull(resul);
            #endregion
        }
    }
    public class FakeUserManager : UserManager<IdentityUser>
    {
        public FakeUserManager()
            : base(
                  new Mock<IUserStore<IdentityUser>>().Object,
                  new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<IdentityUser>>().Object,
                  new IUserValidator<IdentityUser>[0],
                  new IPasswordValidator<IdentityUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }
    }

    public class FakeSignInManager : SignInManager<IdentityUser>
    {
        public FakeSignInManager()
            : base(
                  new Mock<FakeUserManager>().Object,
                  new HttpContextAccessor(),
                  new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                  new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                  new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>().Object,
                  new Mock<Microsoft.AspNetCore.Identity.IUserConfirmation<IdentityUser>>().Object
                  )
        { }
    }
}
