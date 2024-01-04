Shader "TransparentDiffuse"
{
	Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha  // Configuración de mezcla para transparencia
        ZWrite Off  // Desactivamos ZWrite para que los objetos transparentes no oculten a otros objetos
        
        CGPROGRAM
        #pragma surface surf Standard alpha:fade  // Especificamos "alpha:fade" para que el shader sea considerado transparente

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
        
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        
        ENDCG
    }
    Fallback "Standard"
}