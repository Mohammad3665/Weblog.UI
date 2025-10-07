using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.IdentityEntities;

namespace Weblog.Core.Domain.Entities
{
    public class Comment
    {
        [Required]
        public Guid Id { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }

        [Required]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
