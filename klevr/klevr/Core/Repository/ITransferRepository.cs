using System;
using System.Collections.Generic;
using klevr.Concrete.EF;

namespace klevr.Core.Repository
{
    public interface ITransferRepository
    {
        public List<Transfer> GetUserDailyTransfers(string userId);
    }
}
