// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Ghost" {
Properties {
	_Color("Main Color", Color) = (1,1,1,1)

	_minAlpha("Minimum Alpha", Float) = 0.1
	_fadeTime("Fade Alpha Time", Float) = 2
}

SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend Off
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha:blend
		#pragma target 3.0

		float4 _Color;
		float _minAlpha;
		float _fadeTime;
		

		struct Input {
		    float2 uv_MainTex;
			float2 uv_FlowMap;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float alpha = lerp(_minAlpha, _Color.a, abs(_CosTime[3]/_fadeTime));
			o.Alpha = alpha;
    		o.Emission =  _Color;
		}
		ENDCG
	} 
	FallBack "Reflective/Bumped Specular"

}
