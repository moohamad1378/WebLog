using Application.CQRS.UsersCQRS.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        public void Test_AddUserHandler()
        {
            #region Arrang
            var moq = MockHelpers.MockUserManager<IdentityUser>();
            AddUserHandler addUserHandler = new AddUserHandler(moq.Object);
            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase("Test")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var database = new DataBaseContext(options);
            AddUserCommandRequestDto addUserCommandRequestDto = new AddUserCommandRequestDto
            {
                UserName = "Test123@",
                Password = "Test@451",
                Email = "Test@451",
              
            };
            var hasher = new PasswordHasher<AddUserCommandRequestDto>();
           var hasched = hasher.HashPassword(addUserCommandRequestDto,addUserCommandRequestDto.Password);
                 IdentityUser user = new IdentityUser()
                 {
                    UserName = "Test123@",
                    Email = "Test@451",
                 };
            var gg= moq.Setup(p => p.CreateAsync(user, addUserCommandRequestDto.Password).Result)
                .Returns();
            database.SaveChanges();
            
            AddUserCommand addUserCommand = new AddUserCommand(addUserCommandRequestDto);
            CancellationToken cancellationToken = new CancellationToken();

            #endregion
            
            #region Act
            var result= addUserHandler.Handle(addUserCommand, cancellationToken);
            database.SaveChanges();
            #endregion
            #region Assert
            Assert.NotNull(result.Result);
            #endregion
            database.Dispose();
        }
    }
}
