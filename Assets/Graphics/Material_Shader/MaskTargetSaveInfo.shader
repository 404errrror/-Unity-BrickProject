﻿Shader "Hidden/MaskTargetSaveInfo"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color (white = none)", COLOR) = (1, 1, 1, 1)
	}
		SubShader
	{
		// No culling or depth
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off

		Pass
	{
		Stencil
	{
		Ref 5
		Comp Equal
		Pass Replace
		ZFail decrWrap
	}
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float4 color : COLOR;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 color : COLOR;
		float4 vertex : SV_POSITION;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		o.color = v.color;
		return o;
	}

	sampler2D _MainTex;
	float4 _Color;

	fixed4 frag(v2f i) : SV_Target
	{
		float4 col = tex2D(_MainTex, i.uv);

		return col * i.color * _Color;
	}
		ENDCG
	}
	}
}
