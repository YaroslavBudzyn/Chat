using System.ComponentModel.DataAnnotations;

namespace ChatTest.User
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "ID is required.")]
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
