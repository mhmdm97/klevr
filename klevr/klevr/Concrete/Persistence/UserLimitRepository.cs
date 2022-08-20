using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using klevr.Concrete.EF;
using klevr.Core.Repository;
using klevr.Helpers;
using klevr.InMemoryCacheModule;
using System.Linq;
using Microsoft.Extensions.Options;
using klevr.Core.ViewModels;

namespace klevr.Concrete.Persistence
{
    public class UserLimitRepository : IUserLimitRepository
    {
        private readonly DBContext _db;

        private readonly ITransferRepository _transferRepository;

        private readonly ICacheService _cacheService;
        private readonly CacheSettings _cacheSettings;
        private readonly string cacheKey = "userLimits";

        public UserLimitRepository(IOptions<CacheSettings> cacheSettings, ICacheService cacheService, ITransferRepository transferRepository, DBContext db)
        {
            _cacheSettings = cacheSettings.Value;
            _cacheService = cacheService;

            _db = db;

            _transferRepository = transferRepository;
        }

        public List<UserLimits> GetUserLimits()
        {
            try
            {
                //check cache for userLimits
                List<UserLimits> userLimits = GetCachedUserLimits();
                if (userLimits != null) return userLimits;

                //if cache expired/empty fill it will new limits and return data
                userLimits = _db.UserLimits.ToList();
                SetCachedUserLimits(userLimits);
                return userLimits;
            }
            catch(Exception) { }
            return null;
        }
        public List<UserLimits> GetCachedUserLimits()
        {
            try
            {
                return _cacheService.GetData<List<UserLimits>>(cacheKey);
            }
            catch(Exception) { }
            return null;
        }
        public bool SetCachedUserLimits(List<UserLimits> userLimits)
        {
            try
            {
                _cacheService.SetData(cacheKey, userLimits, DateTimeOffset.Now.AddMinutes(_cacheSettings.CachingExpirationMinutes));
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public BaseViewModel CheckTransactionValidity(Guid userId, double transactionAmount)
        {
            try
            {
                var userLimit = _db.UserLimits.Where(uL => uL.UserId == userId).FirstOrDefault();
                if (userLimit == null)
                    return new BaseViewModel { Success = false, Message = "We ran into an error!" };
                //check transaction limit 
                if (transactionAmount > userLimit.TransactionAmountLimit)
                    return new BaseViewModel { Success = false, Message = "Transaction over limit" };

                //check daily limit
                var transactions = _transferRepository.GetUserDailyTransfers(userId);
                double transactionsSum = 0;
                foreach (var transaction in transactions)
                {
                    transactionsSum += transaction.TransferAmount;
                }
                if (transactionsSum + transactionAmount > userLimit.DailyAmountLimit)
                    return new BaseViewModel { Success = false, Message = "Transaction would pass daily limit" };

                return new BaseViewModel { Success = true, Message = "Transaction is valid" };
            }
            catch(Exception) { }
            return new BaseViewModel { Success = false, Message = "We ran into an error!" };
        }
    }
}
