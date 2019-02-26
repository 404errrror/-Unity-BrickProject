﻿Shader "Custom/MaskEffect/TargetImage"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color (white = none)", COLOR) = (1, 1, 1, 1)
	}
	SubShader
		{
			Tags{ "RenderType" = "Transparent" }
			LOD 100

			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite off

			// picture layer
			Pass
					{
						Stencil
						{
							Ref 2
							Comp equal
							Pass keep
							Fail decrWrap
							ZFail keep
						}

						CGPROGRAM
#pragma vertex vert
#pragma fragment frag
						// make fog work
#pragma multi_compile_fog

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
								UNITY_FOG_COORDS(1)
									float4 vertex : SV_POSITION;
							};

							sampler2D _MainTex;
							float4 _MainTex_ST;
							float4 _Color;

							v2f vert(appdata v)
							{
								v2f o;
								o.vertex = UnityObjectToClipPos(v.vertex);
								o.color = v.color;
								o.uv = v.uv;
								UNITY_TRANSFER_FOG(o, o.vertex);
								return o;
							}

							fixed4 frag(v2f i) : SV_Target
							{
								// sample the texture
								fixed4 col = tex2D(_MainTex, i.uv);
								col.rgb *= i.color.rgb * _Color.rgb;
								col.a *= i.color.a * _Color.a
								// apply fog
								UNITY_APPLY_FOG(i.fogCoord, col);
								return col;
							}
								ENDCG
					}
		}
}