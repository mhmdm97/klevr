using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using klevr.Concrete.EF;

namespace klevr.Core.Repository
{
    public interface ITransferRepository
    {
        public List<Transfer> GetUserDailyTransfers(Guid userId);
        public Task<bool> ExecuteNewTransferAsync(Guid userId, Guid targetId, double transferAmount);
        public List<Transfer> GetBatchOfTransfersOverPeriod(DateTime start, DateTime end);
    }
}
