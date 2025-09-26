using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Weblog.Core.Domain.Entities
{
    public class Comment
    {
        [Required]
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }

        [Required]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
