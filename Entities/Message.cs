using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string AddedBy { get; set; }

        [StringLength(50)]
        public string Mssage { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
