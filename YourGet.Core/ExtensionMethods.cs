using System.Collections.Concurrent;

namespace YourGet.Core
{
    /// <summary>
    /// General extension methods
    /// 
    /// TODO: Move some of these to SwissKnife library
    /// </summary>
    public static class ExtensionMethods
    {
        public static void AddOrSet<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key, TValue val)
        {
            self.AddOrUpdate(key, val, (_, __) => val);
        }
    }
}