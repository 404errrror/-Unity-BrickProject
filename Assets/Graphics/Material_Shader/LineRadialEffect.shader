Shader "Hidden/LineRadialEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			stencil
			{
				Ref 4
				Comp Equal
				Pass Keep
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}
			
			sampler2D _MainTex;
			fixed4 frag (v2f i) : SV_Target
			{
				// just invert the colors

				float distance = length(float2(0.5f,0.5f) - i.uv);
				distance = 0.5f - abs(distance);
				distance = saturate(distance);

				float3 rgb = i.color.rgb;
					float a = i.color.a;

				return float4(rgb,a * distance * 2);
			}
			ENDCG
		}
	}
}
