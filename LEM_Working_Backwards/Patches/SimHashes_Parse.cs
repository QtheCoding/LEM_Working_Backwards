using HarmonyLib;
using LEM_Working_Backwards.Utilities;
using System;

// Basically String to SimHashes, the reverse of SimHashes to String file. Called "Parse" because it is used to parse a string into a SimHashes enum value,
// and because it is the method that is called when you use `Enum.Parse` on a SimHashes enum.

namespace LEM_Working_Backwards.Patches
{
    [HarmonyPatch(typeof(Enum), "Parse", new System.Type[] { typeof(System.Type), typeof(string), typeof(bool) })]
    internal class SimHashes_Parse
    {
        private static bool Prefix(System.Type enumType, string value, ref object __result)
        {
            return !enumType.Equals(typeof(SimHashes)) || !SimHashUtil.ReverseSimHashNameLookup.TryGetValue(value, out __result);
        }
    }
}
