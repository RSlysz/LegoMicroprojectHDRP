using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor.Rendering.HighDefinition;                             // for keywords

[ScriptedImporter(1, "customcube")]
public class CustomCubeImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        //Parse the color: Suposedly we have one chanel per line
        string[] lines = File.ReadAllLines(ctx.assetPath);
        if(lines.Length != 3)
            throw new System.Exception("The channel amount should be 3! (R G B)");

        var colorChannels = lines.Select(l =>
        {
            byte result;
            if (!byte.TryParse(l, out result))
                throw new System.Exception("The channel don't contains a byte!");
            return result;
        }).ToArray();
        Color color = new Color32(colorChannels[0], colorChannels[1], colorChannels[2], 255);
        
        //Create objects
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = cube.GetComponent<Renderer>();

        // Set the material v1
        //Material material = new Material(Shader.Find("HDRP/Lit"));
        //HDShaderUtils.ResetMaterialKeywords(material);                // HDRP specific. Very important!
        //material.SetColor("_BaseColor", color);
        //renderer.sharedMaterial = material;

        // Set the material v2
        Material material = new Material(renderer.sharedMaterial);
        material.SetColor("_BaseColor", color);
        renderer.sharedMaterial = material;

        //Register the prefab
        ctx.AddObjectToAsset("Custom Cube", cube);
        ctx.AddObjectToAsset("Custom Cube Material", material);         // Important to not loss the Material instance
        ctx.SetMainObject(cube);
    }
}
