Shader "Hidden/InnerGradient"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Distance("Distance", Range(0, 1)) = 0.5
		_Strenth("Strenth", Range(0, 1)) = 1
	}
	SubShader
	{
		// No culling or depth
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
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
				float4 vertex : SV_POSITION;
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
			float4 _Color;
			float _Distance;
			float _Strenth;

			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				float left = i.uv.x;
				float right = 1 - i.uv.x;
				float top = 1 - i.uv.y;
				float bottom = i.uv.y;

				float distance = _Distance - min(min(left, right), min(top, bottom));
				distance = saturate(distance);

				return i.color * distance * (_Strenth / _Distance);
			}
			ENDCG
		}
	}
}
