// adapted from:
// https://www.youtube.com/watch?v=kV4IG811DUU&t=250s
// https://danielilett.com/2019-05-29-tut2-intro/
Shader "CelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Albedo color", Color) = (1, 1, 1, 1)

        // control layer of shades (flat shading)
        _Shades ("Shades", Range(1, 20)) = 3

        // for the outline
        _OutlineSize("Outline Size", Float) = 0.01
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)

        _Antialiasing("Band Smoothing", Float) = 5.0
		_Glossiness("Glossiness/Shininess", Float) = 400
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
			#include "Lighting.cginc"

            struct appdata
            {
                float2 uv: TEXCOORD0;
                float4 vertex : POSITION;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv: TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal: NORMAL;
                float3 viewDir: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Shades;
            float _Antialiasing;
            float _Glossiness;


            v2f vert (appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 albedo = tex2D(_MainTex, i.uv) * _Color;
				float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);

                // calculate diffuse lighting with toon shader effect
                float diffuse = dot(normal, _WorldSpaceLightPos0);
                float delta = fwidth(diffuse) * _Antialiasing;
                diffuse = smoothstep(0, delta, diffuse);
                //diffuse = floor(diffuse * _Shades) / _Shades;

                // create a toon shading effect

                // calculate specular lighting
                float3 halfVec = normalize(_WorldSpaceLightPos0 + viewDir);
                float specular = dot(normal, halfVec);
                specular = pow(specular * diffuse, _Glossiness);
                specular = smoothstep(0, 0.01 * _Antialiasing, specular);

                // calculate rim lighting

                /*
                float delta = fwidth(diffuse) * _Antialiasing;
                float diffuseSmooth = smoothstep(0, delta, diffuse);

                */

                fixed4 col = albedo * ((diffuse + specular) * _LightColor0 + unity_AmbientSky);

                return col;
            }
            ENDCG
        }

        // draw the stencil of the object on the second pass to create an outline
        Pass
        {
            // draw back faces instead of front faces
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _OutlineSize;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;

                // translate vertex along the normal vector
                // makes stencil bigger so the "outline" is thicker
                float3 normal = normalize(v.normal) * 0.01;

                o.vertex = UnityObjectToClipPos(v.vertex + normal);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    // setting a fallback makes the shadows appear correctly
    FallBack "Diffuse"
}
