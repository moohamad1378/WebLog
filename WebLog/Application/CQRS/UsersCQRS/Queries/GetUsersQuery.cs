using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.UsersCQRS.Queries
{
    public class GetUsersQuery:IRequest<List<GetUsersQueryRespons>>
    {
    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersQueryRespons>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public GetUsersQueryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager=userManager;
        }
        public Task<List<GetUsersQueryRespons>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result=_userManager.Users
                .Select(p=>new GetUsersQueryRespons
                {
                    UserID=p.Id,
                    UserName=p.UserName,
                }).ToList();
            return Task.FromResult(result);
        }
    }
    public class GetUsersQueryRespons
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string DateInsert { get; set; }
    }
}
