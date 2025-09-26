using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weblog.Core.Domain.Entities
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        // Navigation
        public List<Post> Posts { get; set; } = new();
    }
}
