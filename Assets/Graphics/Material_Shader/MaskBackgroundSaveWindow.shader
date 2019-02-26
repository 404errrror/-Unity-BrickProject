Shader "Hidden/MaskBackgroundSaveWindow"
{
	Properties
	{
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
			Comp Always
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
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float4 color : COLOR;
		};

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.color = v.color;
			return o;
		}


		fixed4 frag(v2f i) : SV_Target
		{
			//float4 color = tex2D(_MainTex, i.uv);

			return i.color;
		}
			ENDCG
		}
	}
}
