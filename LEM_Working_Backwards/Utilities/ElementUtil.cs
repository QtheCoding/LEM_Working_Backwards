using HarmonyLib;
using STRINGS;
using System;
using UnityEngine;

namespace LEM_Working_Backwards.Utilities
{
    public static class ElementUtil
    {
        public static void RegisterElementStrings(string elementId, string name, string description)
        {
            string upper = elementId.ToUpper();
            Strings.Add($"STRINGS.ELEMENTS.{upper}.NAME", UI.FormatAsLink(name, upper));
            Strings.Add($"STRINGS.ELEMENTS.{upper}.DESC", description);
        }

        public static KAnimFile FindAnim(string name)
        {
            KAnimFile anim1 = Assets.Anims.Find((Predicate<KAnimFile>)(anim => anim.name == name));
            if ((UnityEngine.Object)anim1 == (UnityEngine.Object)null)
                Debug.LogError((object)("Failed to find KAnim: " + name));
            return anim1;
        }

        public static void AddSubstance(Substance substance)
        {
            Assets.instance.substanceTable.GetList().Add(substance);
        }

        public static Substance CreateSubstance(
          string name,
          Element.State state,
          KAnimFile kanim,
          Material material,
          Color32 color)
        {
            return ModUtil.CreateSubstance(name, state, kanim, material, color, color, color);
        }
        //public static Substance ModUtil.CreateSubstance(string name, Element.State state, KAnimFile kanim, Material material, Color32 colour, Color32 ui_colour, Color32 conduit_colour)
        //{
        //    return new Substance
        //    {
        //        name = name,
        //        nameTag = TagManager.Create(name),
        //        elementID = (SimHashes)Hash.SDBMLower(name),
        //        anim = kanim,
        //        colour = colour,
        //        uiColour = ui_colour,
        //        conduitColour = conduit_colour,
        //        material = material,
        //        renderedByWorld = ((state & Element.State.Solid) == Element.State.Solid)
        //    };
        //}

        public static Substance CreateRegisteredSubstance(
          string name,
          Element.State state,
          KAnimFile kanim,
          Material material,
          Color32 color)
        {
            Substance substance = ElementUtil.CreateSubstance(name, state, kanim, material, color);
            Traverse.Create((object)substance).Field("anims").SetValue((object)new KAnimFile[1]
            {
      kanim
            });
            SimHashUtil.RegisterSimHash(name);
            ElementUtil.AddSubstance(substance);
            ElementLoader.FindElementByHash(substance.elementID).substance = substance;
            return substance;
        }
    }
}
