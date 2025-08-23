using HarmonyLib;
using LEM_Working_Backwards.ElementsData;
using LEM_Working_Backwards.Utilities;
using STRINGS;

namespace LEM_Working_Backwards.Patches
{
    [HarmonyPatch(typeof(Assets), "SubstanceListHookup")]
    internal class Assets_SubstanceListHookup
    {
        private static void Prefix()
        {
            ElementUtil.RegisterElementStrings(
                "SolidBorax", 
                "Borax", 
                $"Borax, also known as sodium borate, is an important boron compound, mainly used in the manufacture of fiberglass and as a flux in metallurgy.");
            ElementUtil.RegisterElementStrings(
                "Spodumene",
                "Spodumene",
                "Spodumene is a pyroxene mineral consisting of lithium aluminum inosilicate. It is an important source of lithium, used in batteries and ceramics.");
        }

        private static void Postfix()
        {
            BoraxElement.RegisterSolidBoraxSubstance();
            SpodumeneElement.RegisterSpodumeneSubstance();
        }
    }
}
