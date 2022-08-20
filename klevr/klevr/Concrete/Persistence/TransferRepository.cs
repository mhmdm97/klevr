using System;
using System.Collections.Generic;
using klevr.Concrete.EF;
using klevr.Core.Repository;
using System.Linq;

namespace klevr.Concrete.Persistence
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DBContext _db;
        public TransferRepository(DBContext db)
        {
            _db = db;
        }
        public List<Transfer> GetUserDailyTransfers(string userId)
        {
            try
            {
                List<Transfer> transfers = _db.Transfers.Where(t => t.OriginUserId == userId).ToList();
                return transfers;
            }
            catch(Exception) { }
            return null;
        }
    }
}
