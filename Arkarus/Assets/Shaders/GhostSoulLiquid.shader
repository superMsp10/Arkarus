// Animated Water Shader
// Copyright (c) 2012, Stanislaw Adaszewski (http://algoholic.eu)
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met: 
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer. 
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution. 
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
Shader "Custom/GhostSoulLiquid"{
	Properties {
		_MainTex ("Texture 1", 2D) = "white" {}
		_MainTex2 ("Texture 2", 2D) = "white" {}

		_FlowMap ("Flow Map", 2D) = "white" {}
		_NoiseMap ("Noise Map", 2D) = "black" {}

		_c1("Color 1", Color) = (1,1,1,1)
		_c2("Color 2", Color) = (1,1,1,1)

	
		_Cycle ("Cycle", float) = 1.0
		_Speed ("Speed", float) = 0.05
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;

		sampler2D _FlowMap;
		sampler2D _NoiseMap;
		float _Cycle;
		float _Speed;
		float3 _c1;
		float3 _c2;
		

		struct Input {
		    float2 uv_MainTex;
			float2 uv_FlowMap;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) {
			// float3 flowDir = tex2D(_FlowMap, IN.uv_FlowMap) * 2 - 1;
			// flowDir *= _Speed;
			float3 noise = tex2D(_NoiseMap, IN.uv_FlowMap);
			
			float phase = _Time[1] / _Cycle + noise.r * _SinTime;
			float f = frac(phase);

			float3 t1 = tex2D (_MainTex, IN.uv_MainTex);
			float3 t2 = tex2D (_MainTex2, IN.uv_MainTex);

			if (f > 0.5f)
				f = 2.0f * (1.0f - f);
			else
				f = 2.0f * f;
			
			o.Normal = 1;
    		// o.Albedo = lerp(t1,t2,f) * lerp(_c1,_c2,f);
			o.Albedo = lerp(_c1,_c2,f);
    		o.Emission =  o.Albedo;

		}
		ENDCG
	} 
	FallBack "Reflective/Bumped Specular"
}
