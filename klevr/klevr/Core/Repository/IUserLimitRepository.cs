using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using klevr.Concrete.EF;

namespace klevr.Core.Repository
{
    public interface IUserLimitRepository
    {
        public List<UserLimits> GetUserLimits();
    }
}
