using System;
using System.Collections.Generic;
using klevr.Concrete.EF;
using klevr.Core.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using klevr.Helpers;

namespace klevr.Concrete.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _db;
        public UserRepository(DBContext db)
        {
            _db = db;
        }
        public List<User> GetUsersWithActiveAccounts()
        {
            
            try
            {
                var users = _db.Users
                    .Include(u => u.Accounts.Where(a => a.AccountStatus == (int)AccountStatus.Active))
                    .Where(u => u.Accounts.Count > 0).ToList();
                return users;
            }
            catch(Exception ex) { }
            return null;
        }
    }
}
