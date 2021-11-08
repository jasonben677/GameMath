Shader "Exam/ShaderExam02"
{
    Properties
    {
       _ManTex("Main Texture", 2D) = "white" {}
       _Color("Color", Color) = (0.25, 0.5, 0.5, 1)
    }

        SubShader
       {
           Tags
           {
               "Queue" = "Transparent"
           }
           Pass
           {
               Blend SrcAlpha OneMinusSrcAlpha

               CGPROGRAM
               #pragma vertex vert
               #pragma fragment frag

               #include "UnityCG.cginc"

               sampler2D _ManTex;
               float4 _Color;


               struct appdata
               {
                   float4 vertex : POSITION;
                   float2 uv : TEXCOORD0;
               };

               struct v2f
               {
                   float4 vertex : SV_POSITION;
                   float2 uv : TEXCOORD1;
               };

               v2f vert(appdata v)
               {
                   v2f o;
                   o.vertex = UnityObjectToClipPos(v.vertex);
                   o.uv = v.uv;
                   return o;
               }

               float4 frag(v2f i) : SV_Target
               {

                   float4 color = tex2D(_ManTex, i.uv);
                   color *= float4(i.uv.r, i.uv.g, 0, 1);
                   return color;
               }

               ENDCG
           }
       }
}
