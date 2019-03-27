// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Ghost" {
Properties {
	_MainTex ("Base 1", 2D) = "white" {}
	_MainTex2 ("Base 2", 2D) = "white" {}

	_Colour1("Colour 1", Color) = (1,1,1,1)
	_minAlpha("Minimum Alpha", Float) = 0.1
	_fadeTime("Fade Alpha Time", Float) = 2
}

SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha:blend
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		float4 _Colour1;
		float _minAlpha;
		float _fadeTime;


		// sampler2D _FlowMap;
		// sampler2D _NoiseMap;
		// float _Speed;
	
		

		struct Input {
		    float2 uv_MainTex;
			float2 uv_FlowMap;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) {
		
			float2 newUV = float2(IN.uv_MainTex.x - _SinTime[1]/_fadeTime, IN.uv_MainTex.y - _CosTime[1]/_fadeTime);

			float3 t1 = tex2D (_MainTex, newUV);
			float3 t2 = tex2D (_MainTex2, IN.uv_MainTex);
			float alpha = lerp(_minAlpha, _Colour1.a, abs(_CosTime[3]/_fadeTime));
			
			o.Normal = 0;
    		o.Albedo = lerp(t1, t2, _SinTime[3]) * _Colour1;
			o.Alpha = alpha;
    		o.Emission =  o.Albedo;

		}
		ENDCG
	} 
	FallBack "Reflective/Bumped Specular"

}
