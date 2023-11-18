// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/Kino" {

	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_top("Top Bar", float) = 0.25
		_bottom("Bottom Bar", float) = 0.25
		_right("Right bar", float) = 0.25
		_left("Left bar", float) = 0.25
		_color("Stripes Color", float) = 0.25
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	sampler2D _MainTex;
	float _top;
	float _bottom;
	float _right;
	float _left;
	float4 _color;

	struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 uv		: TEXCOORD0;
	};

	struct v2f
	{
		float2 uv		: TEXCOORD0;
		float4 vertex   : SV_POSITION;
		fixed4 color	: COLOR;
	};

	v2f vert(appdata_t i)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(i.vertex);
		o.uv = i.uv;
		o.color = i.color;
		return o;
	}

	float4 frag(v2f_img i) : COLOR
	{
		float4 sum = (float4)0;
		sum = tex2D(_MainTex, i.uv);

		float4 border = (float4)0;
#ifdef Cinematic
		border.y = i.uv.y;
		sum.xyz = (border.y > _bottom && border.y < _top) ? sum.xyz : _color;
		return sum;
#endif
#ifdef Corridor
		border.x = i.uv.x;
		sum.xyz = (border.x > _left && border.x < _right) ? sum.xyz : _color;
		return sum;
#endif
#ifdef Scarf
		border.y = i.uv.y;
		sum.xyz = (border.y > _bottom && border.y < _top) ? sum.xyz : _color;
		border.x = i.uv.x;
		sum.xyz = (border.x > _left && border.x < _right) ? sum.xyz : _color;
		return sum;
#endif                                                            
	}

	ENDCG

	SubShader
	{
		Pass
		{
			ZTest Always
			Cull Off
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile Cinematic Corridor Scarf
			ENDCG
		}
	}
}
