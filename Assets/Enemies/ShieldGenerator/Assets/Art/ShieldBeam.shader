Shader "Unlit/ShieldBeam"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
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
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Length;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            float sinSDF(float2 uv)
            {
                uv.y *= 2;
                uv.y -= .5;
                return 1 - abs(uv.y - ((sin(uv.x) + 1) / 2));
            } 

            static float scrollSpeeds[5] = {
                .5112,
                .4123,
                .8123,
                .954,
                .52,
            };

            static float frequencies[5] = {
                1.4123,
                1.5123,
                1.5112,
                0.12,
                0.754,
            };


            #define SINWIDTH .2
            #define SINSHARPNESS 1.5
            fixed4 frag (v2f i) : SV_Target
            {                
                i.uv.x *= _Length;
                fixed4 col = 0;
                for(int j = 0; j < 5; j++)
                {
                    float2 sampleUV = float2(-_Time.x * 10 * scrollSpeeds[j] * 3.141, 0) + i.uv * float2(frequencies[j], 1);
                    float value = sinSDF(sampleUV);
                    value -= 1 - SINWIDTH;
                    value /= (SINWIDTH);
                    value *= SINSHARPNESS;
                    value = min(1, value);
                    col = max(col, value);
                } 
                return col * _Color;
            }
            ENDCG
        }
    }
}
