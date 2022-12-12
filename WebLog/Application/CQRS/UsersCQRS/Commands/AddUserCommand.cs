using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UsersCQRS.Commands
{
    public class AddUserCommand:IRequest<AddUserCommandResponse>
    {

        public AddUserCommand(AddUserCommandRequestDto addUserCommandRequestDto)
        {
            addUserCommandRequest = addUserCommandRequestDto;
        }
        public AddUserCommandRequestDto addUserCommandRequest { get; set; }
    }
    public class AddUserHandler : IRequestHandler<AddUserCommand, AddUserCommandResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AddUserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager= userManager;
        }
        public  Task<AddUserCommandResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = request.addUserCommandRequest.UserName,
                Email = request.addUserCommandRequest.Email
            };
            var result =  _userManager.CreateAsync(user, request.addUserCommandRequest.Password).Result;
            if (result.Succeeded)
            {
                return  Task.FromResult(new AddUserCommandResponse
                {
                    UserId = user.Id,
                });
            }
            else
            {
                string message = "";
                foreach (var item in result.Errors.ToList())
                {
                    message += item.Description + Environment.NewLine;
                }

                return Task.FromResult(new AddUserCommandResponse
                {
                    Errors = message
                });
            }

        }
    }
    public class AddUserCommandResponse
    {
        public string UserId { get; set; }
        public string Errors { get; set; }
    }
    public class AddUserCommandRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
