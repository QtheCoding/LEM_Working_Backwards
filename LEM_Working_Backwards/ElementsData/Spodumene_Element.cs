using UnityEngine;
using LEM_Working_Backwards.Utilities;

namespace LEM_Working_Backwards.ElementsData
{
    public static class SpodumeneElement
    {
        public const string SPODUMENE_ID = "Spodumene";
        public static readonly Color32 SPODUMENE_COLOR = new Color32(57, 255, 20, 255);
        //new Color32(210, 180, 140, 255); // A light brown color for Spodumene
        public static readonly SimHashes SolidSpodumeneSimHash = (SimHashes)Hash.SDBMLower("Spodumene");

        private static Texture2D TintTextureSpodumeneColor(Texture sourceTexture, string name)
        {
            Texture2D texture2D = LEM_Working_Backwards.Utilities.TextureUtil.DuplicateTexture(sourceTexture as Texture2D);
            Color32[] pixels32 = texture2D.GetPixels32();
            for (int index = 0; index < pixels32.Length; ++index)
            {
                float num = ((Color)pixels32[index]).grayscale * 1.5f;
                pixels32[index] = (Color32)((Color)SPODUMENE_COLOR * num);
            }
            texture2D.SetPixels32(pixels32);
            texture2D.Apply();
            texture2D.name = name;
            return texture2D;
        }

        private static Material CreateSpodumeneMaterial(Material source)
        {
            Material solidSpodumeneMaterial = new Material(source);
            solidSpodumeneMaterial.mainTexture = (Texture)SpodumeneElement.TintTextureSpodumeneColor(solidSpodumeneMaterial.mainTexture, "spodumene");
            solidSpodumeneMaterial.name = "matSpodumene";
            return solidSpodumeneMaterial;
        }

        public static void RegisterSpodumeneSubstance()
        {
            // Reuse an existing vanilla substance as the visual/template source.
            Substance substance = Assets.instance.substanceTable.GetSubstance(SimHashes.Granite);

            // Create + register a new substance with the same anim/material as Igneous Rock.
            ElementUtil.CreateRegisteredSubstance(
                "Spodumene",
                Element.State.Solid,
                ElementUtil.FindAnim("spodumene_kanim"),        // substance.anim (kanim)
                SpodumeneElement.CreateSpodumeneMaterial(substance.material),    // reuse Igneous Rock material
                SPODUMENE_COLOR
            );
        }
    }
}
