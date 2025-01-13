using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Domain.Account
{
    public class SystemUser
    {
        [Key]
        public int SystemUserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;

        public int SystemUserGroupID {  get; set; }
        public virtual SystemUserGroup SystemUserGroup { get; set; }
    }
}
