using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Domain.Account
{
    public class SystemUserAccessRole
    {
        [Key]
        public Guid SystemUserAccessRoleID {  get; set; }

        public int SystemUserGroupID { get; set; }
        public virtual SystemUserGroup SystemUserGroup { get; set; }

        public int SystemObjectID { get; set; }
        public virtual SystemObject SystemObject { get; set; }
    }
}
