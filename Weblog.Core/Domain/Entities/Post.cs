using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Weblog.Core.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? ImageUrl { get; set; }
        // relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; } = new();
    }
}
