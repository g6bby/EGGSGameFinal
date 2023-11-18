Shader "MyShaders/Cuby" {
    
	Properties
	{
		_MainTex("Source Image", 2D) = "" {}
		_scale("Scale Factor", float) = 2.0
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	sampler2D _MainTex;
	float4 _MainTex_TexelSize;
	float _scale;

	struct v2f
	{
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	float4 frag(v2f i) : SV_Target
	{
		float2 texel = _MainTex_TexelSize.xy * _scale;
		float2 uv = i.uv.xy/texel;

		float3 sum = tex2D(_MainTex, floor(uv/8) * 8 * texel).rgb;
		
		return fixed4(sum, 1);
	}

	ENDCG

    SubShader 
	{
        Pass 
		{
            ZTest Always
			Cull Off
			ZWrite Off
            Fog { Mode off }

            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}
