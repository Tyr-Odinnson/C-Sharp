Shader "Custom/4_Effect"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OuterRadius ("Outer Radius", Float) = 2
        _InnerRadius ("Inner Radius", Float) = 1.9
        _WorldPosition ("WorldPosition", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0


        struct Input
        {
            float3 worldPos;
        };

        fixed4 _Color;
        float3 _WorldPosition;
        float _OuterRadius;
        float _InnerRadius;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = _Color;

            float d = distance(_WorldPosition, IN.worldPos);

            if (d < _OuterRadius && d > _InnerRadius) {
                c = fixed4 (3, 1, 4, 1) + (2, 0, 4, 1);
            }

            o.Albedo = c.rgb;
            o.Metallic = 0;
            o.Smoothness = .5;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
