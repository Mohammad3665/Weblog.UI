using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Weblog.Core.Domain.Entities
{
    public class Post
    {
        [Required]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        [Required]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
        // relations
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; } = new();
    }
}
