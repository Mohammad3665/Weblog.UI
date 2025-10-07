using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;

namespace Weblog.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //Navigation
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

        public static implicit operator ApplicationUser?(string? v)
        {
            throw new NotImplementedException();
        }
    }
}
