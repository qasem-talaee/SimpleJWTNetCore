using SimpleJWTNetCore.Application.Infrastracture.Account;
using SimpleJWTNetCore.Database.DBC;
using SimpleJWTNetCore.Domain.Account;
using SimpleJWTNetCore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Application.Repository.Account
{
    public class AccountRepository : IAccount
    {
        private SimpleJWTNetCoreDbContext _db;
        public AccountRepository(SimpleJWTNetCoreDbContext db)
        {
            _db = db;
        }

        #region SystemUser
        public IEnumerable<SystemUser> GetAllUsers()
        {
            // This is Sample
            List<SystemUser> res = new List<SystemUser>();
            res.Add(new SystemUser()
            {
                UserName = "Qasem",
            });
            res.Add(new SystemUser()
            {
                UserName = "Tala",
            });
            return res;
        }
        #endregion

        #region Login Logout
        public JWTGenerateViewModel UserAuthenticate(LoginViewModel model)
        {
            // This is Sample
            JWTGenerateViewModel res = new JWTGenerateViewModel()
            {
                IsAuth = true,
                UserName = model.UserName,
                Roles = new List<string>() { "Admin", "User" }
            };
            return res;
        }
        #endregion
    }
}
