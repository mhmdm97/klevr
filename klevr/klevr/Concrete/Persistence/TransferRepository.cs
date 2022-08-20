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

        public List<Transfer> GetUserDailyTransfers(Guid userId)
        {
            try
            {
                List<Transfer> transfers = _db.Transfers.Where(t => t.OriginUserId == userId && t.TransferDate > DateTime.Now.Date).ToList();
                return transfers;
            }
            catch(Exception) { }
            return null;
        }

        public async Task<bool> ExecuteNewTransferAsync(Guid userId, Guid targetId, double transferAmount)
        {
            try
            {
                //create new transfer
                var transfer = new Transfer();
                transfer.OriginUserId = userId;
                transfer.TargetUserId = targetId;
                transfer.TransferAmount = transferAmount;
                transfer.TransferDate = DateTime.Now;

                await _db.Transfers.AddAsync(transfer);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex) { }
            return false;
        }
    }
}
