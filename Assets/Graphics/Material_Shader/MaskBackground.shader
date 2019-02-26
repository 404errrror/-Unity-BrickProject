// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MaskEffect/MaskBackground" {
	Properties
	{
		_Color("Color (white = none)", COLOR) = (1, 1, 1, 1)
		_MainTex("Texture", 2D) = "white" {}
		_CutOff("Cut off", Range(-0.001, 1)) = 0.1
	}
	SubShader
		{
			Tags{ "RenderType" = "Transparent" "Queue" = "Geometry-100" "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				Stencil
				{
					Ref 2
					Comp always
					Pass replace
					ZFail decrWrap
				}

				CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
					uniform float4 _MainTex_ST;
					float4 _Color;
					float _CutOff;

					struct appdata
					{
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
					};

					struct v2f
					{
						float4 vertex : SV_POSITION;
						float2 uv : TEXCOORD0;
					};

					v2f vert(appdata v)
					{
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.uv = TRANSFORM_TEX(v.uv, _MainTex);
						return o;
					}

					fixed4 frag(v2f i) : SV_Target
					{
						float4 color = tex2D(_MainTex, i.uv);

						color.rgb *= _Color.rgb;
						color.a *= _Color.a;

						return color;
					}
						ENDCG
			}
		}
}