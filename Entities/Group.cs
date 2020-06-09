using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatTest.Entities
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }
    }
}
