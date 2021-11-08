using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderReplacement : MonoBehaviour
{
    public Shader replacementShader;

    private void OnEnable()
    {
        if (this.replacementShader != null)
        {
            this.GetComponent<Camera>().SetReplacementShader(replacementShader, "RenderType");
        }
    }

    private void OnDisable()
    {
        this.GetComponent<Camera>().ResetReplacementShader();
    }
}
