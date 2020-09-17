Shader "Custom/3_TextureScroll"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ScrollSpeed ("scroll Speed", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float2 _ScrollSpeed;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 scrolledUVA = IN.uv_MainTex + ((float)_Time * _ScrollSpeed);
            float2 scrolledUVB = IN.uv_MainTex - ((float)_Time * _ScrollSpeed);

            fixed4 c = tex2D (_MainTex, scrolledUVA) * tex2D (_MainTex, scrolledUVB) * fixed4(2, 4, 4, 1) + fixed4(0, 1, 1, 1);
            o.Albedo = c.rgb;
            o.Metallic = 0;
            o.Smoothness = .5;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
