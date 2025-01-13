using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Domain.Account
{
    public class SystemObject
    {
        [Key]
        public int SystemObjectID { get; set; }
        public int ParentID { get; set; }
        public string SystemObjectTitle { get; set; } = string.Empty;
        public string SystemObjectRole {  get; set; } = string.Empty;
    }
}
