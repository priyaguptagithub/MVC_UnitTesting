using HCL.Blog.Models;
using System;
using System.Collections.Generic;

namespace HCL.Blog.Services
{
    public interface IUserAccountService
    {
        UserAccount Get(int userAccountId);

        string GetFirstName(int userAccountId);

        List<UserAccount> GetAll();
    }
}
