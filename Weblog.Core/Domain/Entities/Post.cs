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
        [StringLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [StringLength(20)]
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(100)]
        public string? ImageUrl { get; set; }
        // relations
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; } = new();
    }
}
