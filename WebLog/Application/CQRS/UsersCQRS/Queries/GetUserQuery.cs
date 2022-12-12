using Application.CQRS.UsersCQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UsersCQRS.Queries
{
    public class GetUserQuery:IRequest<GetUserQueryRespons>
    {
        public GetUserQuery(GetUserQueryHandlerDto getUserQueryHandlerDto)
        {
            _getUserQueryHandlerDto= getUserQueryHandlerDto;
        }
        public GetUserQueryHandlerDto _getUserQueryHandlerDto { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryRespons>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataBaseContext _context;
        public GetUserQueryHandler(UserManager<IdentityUser> userManager, DataBaseContext context)
        {
            _userManager=userManager;
            _context = context;
        }
        public Task<GetUserQueryRespons> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = _userManager.FindByIdAsync(request._getUserQueryHandlerDto.UserId).Result;
            var ProfileImage = _context.Profiles.Where(p => p.UserId == result.Id)
                .Include(p => p.Picture).FirstOrDefault();
            if(result != null)
            {
                return Task.FromResult(new GetUserQueryRespons
                {
                    UserId = result.Id,
                    UserName = result.UserName,
                    ProfileImageSrc = ProfileImage.Picture.Src
                });
            }
            else
            {
                return null;
            }
        }
    }
    public class GetUserQueryRespons
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProfileImageSrc { get; set; }
    }
    public class GetUserQueryHandlerDto
    {
        public string UserId { get; set; }
    }
}
