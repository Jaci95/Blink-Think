Shader "Custom/RestrictedVisionMask"
{
    Properties
    {
        _HoleCenter ("Hole Center", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Float) = 0.12
        _Softness ("Softness", Float) = 0.03
        _Color ("Color", Color) = (0, 0, 0, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _HoleCenter;
            float _Radius;
            float _Softness;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = distance(i.uv, _HoleCenter.xy);

                // 0 inside the hole, 1 outside the hole
                float alpha = smoothstep(_Radius, _Radius + _Softness, dist);

                return fixed4(_Color.rgb, alpha * _Color.a);
            }
            ENDCG
        }
    }
}