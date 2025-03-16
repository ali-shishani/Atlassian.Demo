using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Data.Entity
{
    public class MoodRatingRecord : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        public int Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedDateUtc{ get; set; }
    }
}
