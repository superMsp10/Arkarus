// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ForceFeild"
{
    Properties
    {
        _maxAlpha("Maximum Alpha", Float) = 0.1
        _extAlpha("Extermination Alpha", Float) = 0.8
        _maxSpotRadius("Max Spot Radius", Float) = 0.1
        _minSpotRadius("Min Spot Radius", Float) = 0.1
        _maxGhostDistance("Max Ghost Distance", Float) = 0.1
        _extRadius("Extermination Radius", Float) = 0.9
        _extSpotRadius("Extermination Spot Radius", Float) = 0.3


    }
    SubShader
    {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                half4 color: COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 ghostsPos[25];
            float4 ghostsColors[25];

            int ghostCount;
            float _maxAlpha, _extAlpha, _maxSpotRadius, _minSpotRadius, _maxGhostDistance, _extRadius, _extSpotRadius;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 pos = i.color;
                fixed4 col = float4(0,0,0,0);
                int ghostsIn = 0;
                for(int i = 0; i < ghostCount; i++){
                    float d = distance(pos.xyz, ghostsPos[i].xyz);
                    
                    float gDistance = ghostsPos[i].w;
                    float spotR = lerp(_maxSpotRadius, _minSpotRadius, gDistance/_maxGhostDistance);
                    if(d < spotR){
                        col.rgb += ghostsColors[i];
                        col.a += lerp(_maxAlpha, 0, d/(spotR));
                        ghostsIn++;
                    }
                    if(gDistance < _extRadius && d < _extSpotRadius){

                        col.rgb = ghostsColors[i];
                        col.a = _extAlpha;
                        return col;
                    }
                }
                return col/max(ghostsIn, 1);
            }
            ENDCG
        }
    }
}
