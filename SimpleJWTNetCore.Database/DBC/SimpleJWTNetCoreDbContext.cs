using Microsoft.EntityFrameworkCore;
using SimpleJWTNetCore.Domain.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Database.DBC
{
    public class SimpleJWTNetCoreDbContext : DbContext
    {
        public SimpleJWTNetCoreDbContext(DbContextOptions options) : base(options) { }

        #region Account
        public DbSet<SystemUser> Sys_SystemUser { get; set; }
        public DbSet<SystemUserGroup> Sys_SystemUserGroup { get; set; }
        public DbSet<SystemObject> Sys_SystemObject { get; set; }
        public DbSet<SystemUserAccessRole> Sys_SystemUserAccessRole { get; set; }
        #endregion
    }
}
