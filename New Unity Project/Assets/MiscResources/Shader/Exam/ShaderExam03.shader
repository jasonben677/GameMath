Shader "Exam/ShaderExam03"
{
    Properties
    {
       _MainTex("Main Texture", 2D) = "white" {}
       _SecondTex("Second Texture", 2D) = "white" {}
       _Magnitude("Magnitude", Range(0,1)) = 0
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

               sampler2D _MainTex;
               sampler2D _SecondTex;
               float _Magnitude;

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
                   float4 main = tex2D(_MainTex, i.uv);
                   float4 second = tex2D(_SecondTex, i.uv);


                   float4 color = lerp(main, second, _Magnitude);
                   return color;
               }

               ENDCG
           }
       }
}
