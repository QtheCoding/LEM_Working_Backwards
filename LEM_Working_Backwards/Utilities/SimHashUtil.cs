

using System.Collections.Generic;

namespace LEM_Working_Backwards.Utilities
{
    public static class SimHashUtil
    {
        public static Dictionary<SimHashes, string> SimHashNameLookup = new Dictionary<SimHashes, string>();
        public static readonly Dictionary<string, object> ReverseSimHashNameLookup = new Dictionary<string, object>();

        public static void RegisterSimHash(string name)
        {
            SimHashes key = (SimHashes)Hash.SDBMLower(name);
            SimHashUtil.SimHashNameLookup.Add(key, name);
            SimHashUtil.ReverseSimHashNameLookup.Add(name, (object)key);
        }
    }
}
