using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatTest.Entities
{
    [Table("Tokens")]
    public class Token
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(500)]
        public string Code { get; set; }

        [StringLength(500)]
        public string RefreshToken { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
