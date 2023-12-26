Shader "TransparentDiffuse"
{
	Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Transparency ("Transparency", Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha  // Configuración de mezcla para transparencia

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade  // Especificamos "alpha:fade" para que el shader sea considerado transparente

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _Transparency;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a * _Transparency;
        }
        
        ENDCG
    }
    Fallback "Diffuse"
}