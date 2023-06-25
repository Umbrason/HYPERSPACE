Shader "Unlit/HealPulse"
{
    Properties
    {
        [HDR] _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float CircleSDF(float2 uv)
            {
                float2 delta = uv - float2(.5, .5);
                return 1 - length(delta) * 2;
            }



            fixed4 frag (v2f i) : SV_Target
            {
                float radius = CircleSDF(i.uv);
                float targetRadius = _Time * 15;
                const float period = .5;
                float distance = fmod(abs(targetRadius - radius), period) / period;
                float c = 1 - distance;
                const float cutoff = .99999;
                float cutoffMask = min(1 - cutoff, max(0, radius)) / (1 - cutoff);
                float finalOpacity = (1- sqrt(c));
                float innerCutoffMask =  ((min(.5, 1 - radius) / .5));
                finalOpacity *= cutoffMask * cutoffMask * innerCutoffMask;
                float inverseRadius = (1 - radius);
                finalOpacity = max(inverseRadius * inverseRadius * inverseRadius * cutoffMask, finalOpacity);
                return _Color * finalOpacity;
            }
            ENDCG
        }
    }
}
