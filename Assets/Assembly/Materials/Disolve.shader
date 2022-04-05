Shader "Custom/Disolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        [HDR] _Emission ("Emission", color) = (0,0,0)

        [Header(Disolve)]
        _DisolveTex("Disolve Texture", 2D) = "white" {}
        _DisolveAmout("Disolve Amout", Range(0, 1)) = 0.5

        [Header(Border)]
        [HDR]_BorderColor("Color", Color) = (1, 1, 1, 1)
        _BorderWidth("Width", Range(0, .5)) = 0.1
        _BorderFadeout("Fadeout", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _DisolveTex;
        

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DisolveTex;
        };

        half _Glossiness;
        half _Metallic;
        half3 _Emission;
        fixed4 _Color;

        float _DisolveAmout;

        float3 _BorderColor;
        float _BorderWidth;
        float _BorderFadeout;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float disolve = tex2D(_DisolveTex, IN.uv_DisolveTex).r;
            disolve = disolve * 0.99;
            float isVisable = disolve - _DisolveAmout;
            clip(isVisable);

            float isBoarder = smoothstep(_BorderWidth + _BorderFadeout, _BorderWidth, isVisable);
            float3 glowBorder = isBoarder * _BorderColor;

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a ;
            o.Emission = _Emission + glowBorder;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
