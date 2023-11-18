// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShaders/RadialBlur" {

	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "" {}
		_strength("Strength", float) = 0
		_samples("Samples", int) = 0
		_centerX("Epicenter X", float) = 0
		_centerY("Epicenter Y", float) = 0
	}

	CGINCLUDE	
	#include "UnityCG.cginc"
	
	struct v2f 
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	float _strength;
	int _samples;
	float _centerX;
	float _centerY;
	
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	float4 frag(v2f i) : SV_Target 
	{
		float4 sum = (float4)0;

		float2 epicenter = float2(_centerX, _centerY);
		float2 rectangle = float2(i.uv - epicenter);
		float dist = sqrt(pow(rectangle.x, 2) + pow(rectangle.y, 2));

		for(int n = 0; n < _samples; n++) 
		{ 
			float scale = 1.0f - _strength * dist * (n / (float)(_samples - 1));
			sum += tex2D(_MainTex, (i.uv - epicenter) * scale + epicenter); 
		} 

		sum /= _samples; 

		return fixed4(sum);
	}

	ENDCG 
	
	Subshader 
	{
		Pass 
		{
			ZTest Always 
			Cull Off 
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		} 
	}
	Fallback off	
}
