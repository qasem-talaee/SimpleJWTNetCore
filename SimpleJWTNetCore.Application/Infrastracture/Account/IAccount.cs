using SimpleJWTNetCore.Domain.Account;
using SimpleJWTNetCore.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Application.Infrastracture.Account
{
    public interface IAccount
    {
        #region SystemUser
        IEnumerable<SystemUser> GetAllUsers();
        #endregion

        #region Login Logout
        JWTGenerateViewModel UserAuthenticate(LoginViewModel model);
        #endregion
    }
}
