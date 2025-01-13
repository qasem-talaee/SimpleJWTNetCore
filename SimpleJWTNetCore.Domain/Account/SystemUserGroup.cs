using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Domain.Account
{
    public class SystemUserGroup
    {
        [Key]
        public int SystemUserGroupID {  get; set; }
        public string SystemUserGroupTitle { get; set; } = string.Empty;
    }
}
