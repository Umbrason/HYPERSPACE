Shader "Unlit/LaserPathPreview"
{
    Properties
    {        
        [HDR]_Color("Color", Color) = (1,0,0,1)
    }
    SubShader
    {        
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 _Color;
            float _Length;

            #define SOFTNESS 1
            float ArrowPattern(float2 uv)
            {
                uv = uv.yx;
                uv.y = abs(10000-uv.y);
                float dy = abs(uv.x - .5) * 2;
                return max(0, min(SOFTNESS, fmod((uv.y * _Length * 2) + _Time * 40, 1) - dy)) / SOFTNESS;
            }


            #define BLINKSTRENGTH .5
            #define BLINKFREQUENCY 80

            fixed4 frag (v2f i) : SV_Target
            {
                float blink = ((cos(_Time * 3.141 * BLINKFREQUENCY) + 1) / 2);
                blink = blink * BLINKSTRENGTH + 1 - BLINKSTRENGTH;
                return ArrowPattern(i.uv) * _Color * blink;
            }
            ENDCG
        }
    }
}
