Shader "Suica/SimpleUnlit"
{
    Properties
    {
    	
    	[NoScaleOffset]_BaseMap ("主贴图", 2D) = "white" {}
        _BaseColor("主颜色", Color) = (1,1,1,1)
		
        [HideInInspector] _Blend("__blend", Float) = 0.0
        [HideInInspector] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
        [HideInInspector] _Cull("__cull", Float) = 2.0
    	
    }
    SubShader
    {
        Pass
        {
        	Tags{ "LightMode" = "SuicaForward" }
        	Name "Unlit"
        	Blend[_SrcBlend][_DstBlend]
        	ZWrite[_ZWrite]
            Cull[_Cull]
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS : POSITION;
                half2 texcoord : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);    SAMPLER(sampler_BaseMap);
            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.texcoord;
                return OUT;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv) * _BaseColor;
                return col;
            }
            ENDHLSL
        }
        
        Pass
        {
            Tags{ "LightMode" = "SuicaForwardPlus" }
        	Name "Unlit"
        	Blend[_SrcBlend][_DstBlend]
        	ZWrite[_ZWrite]
            Cull[_Cull]
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float3 positionOS : POSITION;
                half2 texcoord : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);    SAMPLER(sampler_BaseMap);
            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.texcoord;
                return OUT;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv) * _BaseColor;
                return col;
            }
            ENDHLSL
        }
    }
    CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.SimpleLitGUI"
}