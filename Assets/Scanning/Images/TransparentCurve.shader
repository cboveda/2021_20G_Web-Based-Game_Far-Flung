Shader "Custom/TransparentCurve" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Curvature ("Curvature", Float) = 0.001
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Standard vertex:vert fullforwardshadows alpha:fade
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Curvature;
		fixed4 _Color;
		half _Glossiness;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		
		void vert( inout appdata_full v)
		{            
			float4 vv = mul( unity_ObjectToWorld, v.vertex ); 
			vv.xyz -= _WorldSpaceCameraPos.xyz;
			vv = float4( 0.0f, ((vv.z * vv.z) + (vv.x * vv.x)) * - _Curvature, 0.0f, 0.0f );
			v.vertex += mul(unity_WorldToObject, vv);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Smoothness = _Glossiness;
			o.Alpha = _Color.a;
			o.Emission = half3(0.3, 0.2, 0.25);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
