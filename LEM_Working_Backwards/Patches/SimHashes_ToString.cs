using HarmonyLib;
using LEM_Working_Backwards.Utilities;
using System;

namespace LEM_Working_Backwards.Patches
{
    [HarmonyPatch(typeof(Enum), "ToString", new System.Type[] { })]
    internal class SimHashes_ToString
    {
        private static bool Prefix(ref Enum __instance, ref string __result)
        {
            return !(__instance is SimHashes) || !SimHashUtil.SimHashNameLookup.TryGetValue((SimHashes)__instance, out __result);
        }
    }
}
