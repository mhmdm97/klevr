using System;
using System.Collections.Generic;
using klevr.Concrete.EF;

namespace klevr.Core.Repository
{
    public interface IUserRepository
    {
        public List<User> GetUsersWithActiveAccounts();
    }
}
