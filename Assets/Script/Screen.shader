Shader "Custom/Screen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        scale ("scale", float) = 1.1
        c1 ("c1",float) = -0.05
        c2 ("c2",float) = -0.1
        f1 ("f1",float) = 1.0
        f2 ("f2",float) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            
            sampler2D _MainTex;
            float scale;
            float c1;
            float c2;
            float f1;
            float f2;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 textureCoordinate = i.uv;

                float2 newTextureCoordinate = float2((scale - f1) *f2 + textureCoordinate.x / scale ,(scale - f1) *f2 + textureCoordinate.y /scale);
                

                fixed4 textureColor = tex2D(_MainTex, newTextureCoordinate);
    
                fixed4 shiftColor1 = tex2D(_MainTex, newTextureCoordinate+float2(c1 * (scale - 1.0), c1 *(scale - 1.0)));
    
                fixed4 shiftColor2 = tex2D(_MainTex, newTextureCoordinate+float2(c2 * (scale - 1.0), c2 *(scale - 1.0)));
    
                fixed3 blendFirstColor = fixed3(textureColor.r , textureColor.g, shiftColor1.b);
    
                fixed3 blend3DColor = fixed3(shiftColor2.r, blendFirstColor.g, blendFirstColor.b);
    
                return fixed4(blend3DColor, textureColor.a);

            }
            ENDCG
        }
    }
}