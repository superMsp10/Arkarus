// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ForceFeild"
{
    Properties
    {
        _maxAlpha("Maximum Alpha", Float) = 0.1
        _maxSpotRadius("Max Spot Radius", Float) = 0.1
        _minSpotRadius("Min Spot Radius", Float) = 0.1
        _maxGhostDistance("Max Ghost Distance", Float) = 0.1


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
            float _maxAlpha, _maxSpotRadius, _minSpotRadius, _maxGhostDistance;

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
                fixed4 col = float4(1,1,1,1);
                float min = 10000000, gDistance = 0;
                if(ghostCount > 0){
                    for(int i = 0; i < ghostCount; i++){
                        float d = distance(pos.xyz, ghostsPos[i].xyz);
                        if(d < min){
                            min = d;
                            gDistance = ghostsPos[i].w;
                            col = ghostsColors[i];
                        }
                    }
                    float spotR = lerp(_maxSpotRadius, _minSpotRadius, gDistance/_maxGhostDistance);
                    if(min < spotR){
                        col.a = lerp(_maxAlpha, 0, min/(spotR));
                    }else{
                        col.a = 0;
                    }
                    
                }else{
                    col.a = 0;
                }
                return col;
            }
            ENDCG
        }
    }
}
