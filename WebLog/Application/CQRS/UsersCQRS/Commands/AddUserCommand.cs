using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UsersCQRS.Commands
{
    internal class AddUserCommand:IRequest<AddUserCommandResponse>
    {
        public AddUserCommand(AddUserCommandResponsDto addUserCommandResponsDto)
        {
            AddUserCommandResponsDto= addUserCommandResponsDto;
        }
        public AddUserCommandResponsDto AddUserCommandResponsDto { get; set; }
    }
    public class AddUserHandler : IRequestHandler<AddUserCommandResponsDto, AddUserCommandResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AddUserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager= userManager;
        }
        public Task<AddUserCommandResponse> Handle(AddUserCommandResponsDto request, CancellationToken cancellationToken)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var result = _userManager.CreateAsync(user, request.Password).Result;
            if (result.Succeeded)
            {
                return Task.FromResult(new AddUserCommandResponse
                {
                    UserId = user.Id,
                });
            }
            List<string> message;
            foreach (var item in result.Errors.ToList())
            {
                message.Add += item.Description + Environment.NewLine;
            }
            return Task.FromResult(new AddUserCommandResponse
            {
                Errors = message
            });
        }
    }
    public class AddUserCommandResponse
    {
        public string UserId { get; set; }
        public List<string> Errors { get; set; }
    }
    public class AddUserCommandResponsDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
