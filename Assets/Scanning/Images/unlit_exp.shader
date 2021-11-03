Shader "Unlit/unlit_exp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint("Tint Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0,0.5)) = 0.25
        _Curvature("Curvature", Float) = 0.0003
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Curvature;
            float4 _MainTex_ST;
            float4 _Tint;
            float _Transparency;

            /*v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }*/

            v2f vert (appdata v)
            {            
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float4 vv = mul( unity_ObjectToWorld, v.vertex ); 
                vv.xyz -= _WorldSpaceCameraPos.xyz;
                vv = float4( 0.0f, ((vv.z * vv.z) + (vv.x * vv.x)) * - _Curvature, 0.0f, 0.0f );
                o.vertex += mul(unity_WorldToObject, vv);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Tint;
                col.a = _Transparency;
                return col;
            }
            ENDCG
        }
    }
}
