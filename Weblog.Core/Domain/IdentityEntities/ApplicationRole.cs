using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Weblog.Core.Domain.IdentityEntities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName)
        {
            Id = Guid.NewGuid();
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }
    }
}
