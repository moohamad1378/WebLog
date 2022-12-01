using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Slug { get; set; }
        public Picture Picture { get; set; }
        public int PictureId { get; set; }
    }
}
