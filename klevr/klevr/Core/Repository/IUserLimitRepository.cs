using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using klevr.Concrete.EF;
using klevr.Core.ViewModels;

namespace klevr.Core.Repository
{
    public interface IUserLimitRepository
    {
        public List<UserLimits> GetUserLimits();
        public BaseViewModel CheckTransactionValidity(string userId, double transactionAmount);
    }
}
