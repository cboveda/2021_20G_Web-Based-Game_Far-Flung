Shader "Custom/TransparentCurve" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Curvature ("Curvature", Float) = 0.001
		_RimColor ( "Rim Color", Color ) = (1,1,1,1)
		_RimPower ("Rim Power", Range( 0.5, 10.0 )) = 3.0
		_OutColor("Out Color", Color) = (1,1,1,1)

	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Standard vertex:vert fullforwardshadows alpha:fade
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Curvature;
		fixed4 _Color;
		half _Glossiness;
		half _Metallic;
		float4 _RimColor;
		float _RimPower;
		float4 _OutColor;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};
		
		void vert( inout appdata_full v)
		{            
			float4 vv = mul( unity_ObjectToWorld, v.vertex ); 
			vv.xyz -= _WorldSpaceCameraPos.xyz;
			vv = float4( 0.0f, ((1.5 * (vv.z * vv.z)) + (vv.x * vv.x)) * - _Curvature, 0.0f, 0.0f );
			v.vertex += mul(unity_WorldToObject, vv);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));

			half rim = 1.0 - dot(IN.viewDir, o.Normal);
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);

			o.Albedo = c.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = lerp( _Color, _OutColor, IN.uv_MainTex.y ).a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
