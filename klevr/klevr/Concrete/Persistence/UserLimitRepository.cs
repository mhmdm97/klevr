using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using klevr.Concrete.EF;
using klevr.Core.Repository;
using klevr.Helpers;
using klevr.InMemoryCacheModule;
using System.Linq;
using Microsoft.Extensions.Options;

namespace klevr.Concrete.Persistence
{
    public class UserLimitRepository : IUserLimitRepository
    {
        private readonly DBContext _db;
        private readonly ICacheService _cacheService;
        private readonly CacheSettings _cacheSettings;
        private readonly string cacheKey = "userLimits";

        public UserLimitRepository(IOptions<CacheSettings> cacheSettings, ICacheService cacheService, DBContext db)
        {
            _cacheSettings = cacheSettings.Value;
            _cacheService = cacheService;
            _db = db;
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
    }
}
