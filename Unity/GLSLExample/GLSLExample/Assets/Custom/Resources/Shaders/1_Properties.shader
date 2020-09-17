Shader "Custom/1_Properties"
{
    Properties
    {
        _TestRange("a", Range(0, 1)) = .5
        _TestFloat("b", Float) = 1
        _TestInt("c", Int) = 0
        _TestColor("d", Color) = (.5, .5, .5, 1)
        _TestVector("e", Vector) = (0, 0, 0, 0)
        _TestTexture("f", 2D) = "" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _TestTexture;

        struct Input
        {
            float2 uv_TestTexture;
        };

        fixed4 _TestColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_TestTexture, IN.uv_TestTexture) * _TestColor;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = 0;
            o.Smoothness = .5;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
