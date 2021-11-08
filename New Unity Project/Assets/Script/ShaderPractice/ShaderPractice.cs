using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ShaderPractice : MonoBehaviour
{

    public Material material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null) return;
        Graphics.Blit(source, destination, material);
    }

}
