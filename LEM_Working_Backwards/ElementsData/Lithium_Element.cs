using UnityEngine;
using LEM_Working_Backwards.Utilities;

namespace LEM_Working_Backwards.ElementsData
{
    public static class Lithium_Element
    {
        public static readonly Color32 LITHIUM_COLOR = new Color32((byte)100, (byte)241, (byte)211, byte.MaxValue);
        public const string LITHIUM_ID = "SolidLithium";
        public static readonly SimHashes SolidLithiumSimHash = (SimHashes)Hash.SDBMLower("SolidLithium");

        private static Texture2D TintTextureLithiumColor(Texture sourceTexture, string name)
        {
            Texture2D texture2D = LEM_Working_Backwards.Utilities.TextureUtil.DuplicateTexture(sourceTexture as Texture2D);
            Color32[] pixels32 = texture2D.GetPixels32();
            for (int index = 0; index < pixels32.Length; ++index)
            {
                float num = ((Color)pixels32[index]).grayscale * 1.5f;
                pixels32[index] = (Color32)((Color)Lithium_Element.LITHIUM_COLOR * num);
            }
            texture2D.SetPixels32(pixels32);
            texture2D.Apply();
            texture2D.name = name;
            return texture2D;
        }

        private static Material CreateSolidLithiumMaterial(Material source)
        {
            Material solidLithiumMaterial = new Material(source);
            solidLithiumMaterial.mainTexture = (Texture)Lithium_Element.TintTextureLithiumColor(solidLithiumMaterial.mainTexture, "solidlithium");
            solidLithiumMaterial.name = "matSolidLithium";
            return solidLithiumMaterial;
        }

        public static void RegisterSolidLithiumSubstance()
        {
            Substance substance = Assets.instance.substanceTable.GetSubstance(SimHashes.IgneousRock);
            ElementUtil.CreateRegisteredSubstance(
                "SolidLithium",
                Element.State.Solid,
                substance.anim,
                Lithium_Element.CreateSolidLithiumMaterial(substance.material),
                Lithium_Element.LITHIUM_COLOR);
        }
    }
}
