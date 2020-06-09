﻿
using System.ComponentModel.DataAnnotations;

namespace ChatTest.Role
{
    public class RoleSearchModel
    {
        public int? Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
