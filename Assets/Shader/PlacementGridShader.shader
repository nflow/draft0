Shader "Custom/PlacementGridShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _GridSize ("Grid Size", Int) = 20
        _AlphaIntensity ("Alpha Intensity", Float) = 2.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Cull front 
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            uniform float _AlphaIntensity;
            uniform int _GridSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint id : SV_VertexID;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color: COLOR;
                float2 uv : TEXCOORD0;
            };      

            v2f vert (appdata v)
            {
                v2f o;
                o.color = _Color;
                o.uv = v.uv;
                float1 offset = v.id;

                float1 realGridCount = _GridSize-1;
                float1 fadeRange = _GridSize*0.8;

                float1 x = fmod(offset, _GridSize);
                float1 z = floor(offset / _GridSize);

                o.color.a = min(x/fadeRange, (realGridCount - x)/fadeRange) * min(z/fadeRange, (realGridCount - z)/fadeRange) * _AlphaIntensity;
                // o.color.a = max(0,1-((x-10)*(x-10) + (z-10)*(z-10)) / 50.0);
                // o.color.a = max(0,1-(abs(x-10)+abs(z-10)) / 10.0);
                // o.color.a = max(0,1-max(abs(x-10),abs(z-10)) / 7.0);

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : COLOR {
                float4 c;
                c = i.color;
                return tex2D(_MainTex, i.uv) * c;
            }
            ENDCG
        }
    }
}
