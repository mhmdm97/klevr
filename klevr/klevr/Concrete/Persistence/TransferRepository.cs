using System;
using System.Collections.Generic;
using klevr.Concrete.EF;
using klevr.Core.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace klevr.Concrete.Persistence
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DBContext _db;
        public TransferRepository(DBContext db)
        {
            _db = db;
        }

        public List<Transfer> GetUserDailyTransfers(Guid originId)
        {
            try
            {
                List<Transfer> transfers = _db.Transfers.Where(t => t.OriginAccountId == originId && t.TransferDate > DateTime.Now.Date).ToList();
                return transfers;
            }
            catch(Exception) { }
            return null;
        }

        public async Task<bool> ExecuteNewTransferAsync(Guid originId, Guid targetId, double transferAmount)
        {
            try
            {
                //create new transfer
                var transfer = new Transfer();
                transfer.OriginAccountId = originId;
                transfer.TargetAccountId = targetId;
                transfer.TransferAmount = transferAmount;
                transfer.TransferDate = DateTime.Now;

                await _db.Transfers.AddAsync(transfer);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex) { }
            return false;
        }

        public List<Transfer> GetBatchOfTransfersOverPeriod(DateTime start, DateTime end)
        {
            try
            {
                List<Transfer> transfers = _db.Transfers.Where(t => t.TransferDate > start && t.TransferDate < end).Take(10).ToList();
                return transfers;
            }
            catch(Exception) { }
            return null;
        }
    }
}
