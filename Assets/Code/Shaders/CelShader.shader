/* adapted from :
	https://www.youtube.com/watch?v=kV4IG811DUU&t=250s
	https://danielilett.com/2019-05-29-tut2-intro/
	https://roystan.net/articles/toon-shader/
	https://www.ronja-tutorials.com/post/032-improved-toon/#specular-highlights
*/
Shader "deeznuts/CelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Albedo color", Color) = (1, 1, 1, 1)

        // for multi steps toon shading effect
        [IntRange]_StepAmount("Shadow Steps", Range(1, 16)) = 3
        _StepWidth("Step Size", Range(0, 1)) = 0.2

        // for the outline
        _OutlineSize("Outline Size", Float) = 0.01
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)

        // for extra effects
        _Antialiasing("Band Smoothing", Float) = 5.0
		_Glossiness("Glossiness/Shininess", Float) = 400
		_Fresnel("Fresnel/Rim Amount", Range(0, 1)) = 0.7
    }
    SubShader
    {
        Tags { 
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

            struct vertIn
            {
                float2 uv: TEXCOORD0;
                float4 vertex : POSITION;
                float3 normal: NORMAL;
            };

            struct vertOut
            {
                float2 uv: TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal: NORMAL;
                float3 viewDir: TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _StepWidth;
            float _StepAmount;
            float _Antialiasing;
            float _Glossiness;
            float _Fresnel;

            vertOut vert (vertIn v)
            {
                vertOut o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
				TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag(vertOut i) : SV_Target
            {
                fixed4 albedo = tex2D(_MainTex, i.uv) * _Color;
				float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);

                // to receive shadow
				float shadow = SHADOW_ATTENUATION(i);

                // calculate diffuse lighting with toon shading effect
                float diffuse = dot(normal, _WorldSpaceLightPos0);
                // multiple steps effect
                diffuse = floor(diffuse / _StepWidth);
                float delta = fwidth(diffuse) * _Antialiasing;
                diffuse += smoothstep(0, delta, frac(diffuse));
                // bring it back into range to use it for color
                diffuse /= _StepAmount;
                diffuse = saturate(diffuse) * shadow;

                // calculate specular lighting
                float3 halfVec = normalize(_WorldSpaceLightPos0 + viewDir);
                float specular = dot(normal, halfVec);
                specular = pow(specular * diffuse, _Glossiness);
                specular = smoothstep(0, 0.01 * _Antialiasing, specular) * shadow;

                // calculate rim lighting with fresnel
                float rim = 1 - dot(viewDir, normal);
                rim = rim * diffuse; // control how far the rim extends along the surface
                rim = smoothstep(_Fresnel - 0.01, _Fresnel + 0.01, rim);

                fixed4 col = albedo * ((diffuse + specular + rim) * _LightColor0 + unity_AmbientSky);

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

            struct vertIn
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
            };

            struct vertOut
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineSize;
            float4 _OutlineColor;

            vertOut vert (vertIn v)
            {
                vertOut o;

                // translate vertex along the normal vector
                // makes stencil bigger so the "outline" is thicker
                float3 normal = normalize(v.normal) * _OutlineSize;

                float3 pos = v.vertex + normal;
                o.pos = UnityObjectToClipPos(pos);

                return o;
            }

            fixed4 frag(vertOut i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    // setting a fallback makes the shadows appear correctly
    FallBack "Diffuse"
}
