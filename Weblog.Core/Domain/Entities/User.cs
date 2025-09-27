using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weblog.Core.Domain.Entities
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        //Navigation
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

    }
}
