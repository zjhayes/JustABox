Shader "Custom/SurveillanceEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseIntensity ("Noise Intensity", Range(0,1)) = 0.5
        _ScanLineIntensity ("Scan Line Intensity", Range(0,1)) = 0.5
        _ScanLineCount ("Scan Line Count", Range(1,1000)) = 500
        _WideScanLineCount ("Wide Scan Line Count", Range(1,1000)) = 100
        _ScanLineSpeed ("Scan Line Speed", Range(0,10)) = 1.0
        _WideScanLineSpeed ("Wide Scan Line Speed", Range(0,10)) = 0.5
        _NoiseSpeed ("Noise Speed", Range(0,10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _NoiseIntensity;
            float _ScanLineIntensity;
            float _ScanLineCount;
            float _WideScanLineCount;
            float _ScanLineSpeed;
            float _WideScanLineSpeed;
            float _NoiseSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Simplex noise function
            float random (float2 st) {
                return frac(sin(dot(st.xy, float2(12.9898,78.233))) * 43758.5453123);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Procedural noise
                float2 noiseUV = i.uv * 10.0 + _Time.y * _NoiseSpeed;
                float noise = random(noiseUV);
                fixed4 noiseColor = fixed4(noise, noise, noise, 1.0);

                // Blend the noise
                col.rgb = lerp(col.rgb, noiseColor.rgb, _NoiseIntensity);

                // Apply scan lines
                float scanLine = abs(sin((i.uv.y + _Time.y * _ScanLineSpeed) * _ScanLineCount * 3.14159));
                col.rgb = lerp(col.rgb, col.rgb * scanLine, _ScanLineIntensity);

                // Apply wide scan lines
                float wideScanLine = abs(sin((i.uv.y + _Time.y * _WideScanLineSpeed) * _WideScanLineCount * 3.14159));
                col.rgb = lerp(col.rgb, col.rgb * wideScanLine, _ScanLineIntensity * 0.5);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}