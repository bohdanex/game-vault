using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Services.Extensions
{
    internal static class CacheKeys
    {
        public static readonly CacheKey BestSellers = new("BestSellers", TimeSpan.FromHours(2));
    }

    internal class CacheKey(string keyName)
    {
        public CacheKey(string keyName, TimeSpan lifetime) : this(keyName)
        {
            Lifetime = lifetime;
        }

        public string Name { get; private set; } = keyName;
        public TimeSpan Lifetime { get; set; }

        public DistributedCacheEntryOptions Options => new() 
        { 
             AbsoluteExpirationRelativeToNow = Lifetime
        };
    }
}
