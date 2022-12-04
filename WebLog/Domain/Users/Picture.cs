using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class Picture
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public virtual Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
