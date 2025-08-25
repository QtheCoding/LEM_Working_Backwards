using UnityEngine;
using LEM_Working_Backwards.Utilities;

namespace LEM_Working_Backwards.ElementsData
{
    internal class Galena_Element
    {
        public static readonly Color32 GALENA_COLOR = new Color32((byte)225, (byte)241, (byte)242, byte.MaxValue);
        public const string GALENA_ID = "Galena";
        public static readonly SimHashes GalenaSimHash = (SimHashes)Hash.SDBMLower("Galena");

        private static Texture2D TintTextureGalenaColor(Texture sourceTexture, string name)
        {
            Texture2D texture2D = LEM_Working_Backwards.Utilities.TextureUtil.DuplicateTexture(sourceTexture as Texture2D);
            Color32[] pixels32 = texture2D.GetPixels32();
            for (int index = 0; index < pixels32.Length; ++index)
            {
                float num = ((Color)pixels32[index]).grayscale * 1.5f;
                pixels32[index] = (Color32)((Color)Galena_Element.GALENA_COLOR * num);
            }
            texture2D.SetPixels32(pixels32);
            texture2D.Apply();
            texture2D.name = name;
            return texture2D;
        }

        private static Material CreateGalenaMaterial(Material source)
        {
            Material galenaMaterial = new Material(source);
            galenaMaterial.mainTexture = (Texture)Galena_Element.TintTextureGalenaColor(galenaMaterial.mainTexture, "galena");
            galenaMaterial.name = "matGalena";
            return galenaMaterial;
        }

        public static void RegisterGalenaSubstance()
        {
            Substance substance = Assets.instance.substanceTable.GetSubstance(SimHashes.Rust);
            ElementUtil.CreateRegisteredSubstance(
                "Galena", 
                Element.State.Solid, 
                ElementUtil.FindAnim("raw_galena_kanim"), 
                Galena_Element.CreateGalenaMaterial(substance.material), 
                Galena_Element.GALENA_COLOR);
        }
    }
}
