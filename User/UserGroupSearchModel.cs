
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatTest.User
{
    public class UserGroupSearchModel
    {
        [DefaultValue(0)]
        public int? Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public int? GroupId { get; set; }

    }
}
