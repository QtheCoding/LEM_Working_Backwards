using UnityEngine;
using LEM_Working_Backwards.Utilities;

namespace LEM_Working_Backwards.ElementsData
{
    public static class BoraxElement
    {
        public static readonly Color32 BORAX_COLOR = new Color32((byte)245, (byte)241, (byte)211, byte.MaxValue);
        public const string BORAX_ID = "SolidBorax";
        public static readonly SimHashes SolidBoraxSimHash = (SimHashes)Hash.SDBMLower("SolidBorax");

        private static Texture2D TintTextureBoraxColor(Texture sourceTexture, string name)
        {
            Texture2D texture2D = LEM_Working_Backwards.Utilities.TextureUtil.DuplicateTexture(sourceTexture as Texture2D);
            Color32[] pixels32 = texture2D.GetPixels32();
            for (int index = 0; index < pixels32.Length; ++index)
            {
                float num = ((Color)pixels32[index]).grayscale * 1.5f;
                pixels32[index] = (Color32)((Color)BoraxElement.BORAX_COLOR * num);
            }
            texture2D.SetPixels32(pixels32);
            texture2D.Apply();
            texture2D.name = name;
            return texture2D;
        }

        private static Material CreateSolidBoraxMaterial(Material source)
        {
            Material solidBoraxMaterial = new Material(source);
            solidBoraxMaterial.mainTexture = (Texture)BoraxElement.TintTextureBoraxColor(solidBoraxMaterial.mainTexture, "solidborax");
            solidBoraxMaterial.name = "matSolidBorax";
            return solidBoraxMaterial;
        }

        public static void RegisterSolidBoraxSubstance()
        {
            Substance substance = Assets.instance.substanceTable.GetSubstance(SimHashes.SolidCarbonDioxide);
            ElementUtil.CreateRegisteredSubstance(
                "SolidBorax", 
                Element.State.Solid, 
                ElementUtil.FindAnim("solid_borax_kanim"), 
                BoraxElement.CreateSolidBoraxMaterial(substance.material), 
                BoraxElement.BORAX_COLOR);
        }
    }
}
