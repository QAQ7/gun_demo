Shader "Custom/Dissolve2" {
Properties {
//英文版
//_Color ("Color", Color) = (1,1,1,1)
//_MainTex ("Albedo (RGB)", 2D) = "white" {}
//_Glossiness ("Smoothness", Range(0,1)) = 0.5
//_Metallic ("Metallic", Range(0,1)) = 0.0
//
// _NoiseTex ("NoiseTex (R)",2D) = "white"{}  
//        _EdgeWidth("EdgeWidth",Range(0,0.5)) = 0.1  
//        _EdgeColor("EdgeColor",Color) =  (1,1,1,1)  
//  _EdgeThresholdValue ("EdgeThresholdValue(Zero for not use)",Range(0,1)) = 0.5  
//        _DissolvePercentage ("DissolvePercentage",Range(0,1)) = 0  
//中文版
_Color ("颜色", Color) = (1,1,1,1)
_MainTex ("主贴图 (RGB)", 2D) = "white" {}
_Glossiness ("平滑度", Range(0,1)) = 0.5
_Metallic ("金属性", Range(0,1)) = 0.0
_NoiseTex ("噪声贴图 (R)",2D) = "white"{}  
_EdgeWidth("边缘宽度",Range(0,0.5)) = 0.1  
_EdgeColor("边缘颜色",Color) =  (1,1,1,1)  
_EdgeThresholdValue ("硬边缘阈值(0为不使用)",Range(0,1)) = 0.5  
_DissolvePercentage ("溶解百分比",Range(0,1)) = 0  

_AtmoColor("Atmosphere Color", Color) = (0, 0.4, 1.0, 1)    //光晕颜色
_Size("Size", Float) = 0.1 //光晕范围
_OutLightPow("Falloff",Float) = 5 //光晕平方参数
_OutLightStrength("Transparency", Float) = 15 //光晕强度
}

SubShader {
Tags { "RenderType"="Opaque" }
LOD 200
CGPROGRAM
// Physically based Standard lighting model, and enable shadows on all light types
//原编译指令
//#pragma surface surf Standard fullforwardshadows
//增加addshadow以获得正确的阴影
#pragma surface surf Standard fullforwardshadows addshadow
// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0
sampler2D _MainTex;
sampler2D _NoiseTex;  
float _EdgeWidth;  
float4 _EdgeColor;  
float _EdgeThresholdValue;  
float _DissolvePercentage;
struct Input {
float2 uv_MainTex;
float3 worldPos;
};
half _Glossiness;
half _Metallic;
fixed4 _Color;
void surf (Input IN, inout SurfaceOutputStandard o) {

// Albedo comes from a texture tinted by color
fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
o.Albedo = c.rgb;
// Metallic and smoothness come from slider variables
o.Metallic = _Metallic;
o.Smoothness = _Glossiness;
o.Alpha = c.a;
float DissolveFactor = saturate(_DissolvePercentage);  
//使用固定坐标
            float noiseValue = tex2D(_NoiseTex,IN.uv_MainTex).r;   
            //使用世界坐标
             //float noiseValue = tex2D(_NoiseTex,IN.worldPos.rg).r;

              //如果该值对应噪声图的值更大，则抛弃

            if(noiseValue <= DissolveFactor)  
            {  
                discard;  
            }   
            float4 texColor = tex2D(_MainTex,IN.uv_MainTex);  
            float EdgeFactor = saturate((noiseValue - DissolveFactor)/(_EdgeWidth*DissolveFactor));  
            float4 BlendColor = texColor * _EdgeColor;  

           if(_EdgeThresholdValue>0){
           //不使用渐变（硬边缘）
          float HardEdgeFactor=EdgeFactor;
          if(HardEdgeFactor>_EdgeThresholdValue){
          HardEdgeFactor=1;
          o.Emission =0;  
          }else{ 
          HardEdgeFactor=0;
          o.Emission =_EdgeColor; 
          }
          o.Albedo = lerp(texColor,BlendColor,1-EdgeFactor);  
           }else{
           o.Emission =0; 
           //使用渐变（软边缘）
           if(_EdgeThresholdValue>=1){
             o.Albedo = BlendColor; 
             o.Alpha=0;
           }else{
            o.Albedo = lerp(texColor,BlendColor,1 - EdgeFactor);  
           }
           }

}
ENDCG
}
FallBack "Diffuse"
}

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shader Learn/OutLighting"  //Shader文件索引路径
{
    // 属性
    Properties
    {
        _MainTex("Texture(RGB)", 2D) = "grey" {} //表面贴图 默认灰色
        _Color("Color", Color) = (0, 0, 0, 1)    //为贴图附加的颜色 默认为白色
        _AtmoColor("Atmosphere Color", Color) = (0, 0.4, 1.0, 1)    //光晕颜色
        _Size("Size", Float) = 0.1 //光晕范围
        _OutLightPow("Falloff",Float) = 5 //光晕平方参数
        _OutLightStrength("Transparency", Float) = 15 //光晕强度
    }

    SubShader
    {
        Pass //通道1 用于给物体贴图、填色
        {
            Name "PlaneBase"
            Tags{ "LightMode" = "Always" }
            Cull Back
            //CG程序开始
            CGPROGRAM
//声明顶点着色器函数为vert
#pragma vertex vert
//声明片段着色器函数为frag
#pragma fragment frag
#include "UnityCG.cginc"
            //函数可能用到的参数
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float4 _AtmoColor;
            uniform float _Size;
            uniform float _OutLightPow;
            uniform float _OutLightStrength;
            //顶点着色器的输出
            struct vertexOutput
            {
                float4 pos:SV_POSITION;
                float3 normal:TEXCOORD0;
                float3 worldvertpos:TEXCOORD1;
                float2 texcoord:TEXCOORD2;
            };
            //顶点着色器函数
            vertexOutput vert(appdata_base v)
            {
                vertexOutput o;
                // 顶点位置
                o.pos = UnityObjectToClipPos(v.vertex);
                // 法线
                o.normal = v.normal;
                // 世界坐标顶点位置
                o.worldvertpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                // 纹理
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
            //片段着色器函数
            float4 frag(vertexOutput i) :COLOR
            {   
                float4 color = tex2D(_MainTex, i.texcoord);
                //i.normal = normalize(i.normal);
                ////视角法线
                //float3 viewdir = normalize(i.worldvertpos.xyz - _worldspacecamerapos.xyz);// normalize(i.worldvertpos - _worldspacecamerepos);
                //float4 color0 = _atmocolor;
                ////视角法线与模型法线点积形成中间指为1向四周逐渐衰减为0的点积值，赋给透明通道，形成光晕效果
                //color0.a = _outlightpow*(1 - dot(viewdir, i.normal));
                //color.rgb = lerp(color.rgb, color0.rgb, color0.a);
                // 纹理贴图叠加颜色
                return color*_Color;
            }
            ENDCG
        }

        //通道2： 用于生成模型外部的光晕
        Pass
        {
            Name "AtmosphereBase"
            Tags{ "LightMode" = "Always" }
            Cull Front
            Blend SrcAlpha One

            CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
            uniform float4 _Color;
            uniform float4 _AtmoColor;
            uniform float _Size;
            uniform float _OutLightPow;
            uniform float _OutLightStrength;

            struct vertexOutput
            {
                float4 pos:SV_POSITION;
                float3 normal:TEXCOORD0;
                float3 worldvertpos:TEXCOORD1;
            };

            vertexOutput vert(appdata_base v)
            {
                vertexOutput o;
                //顶点位置以法线方向向外延伸
                v.vertex.xyz += v.normal*_Size;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.worldvertpos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag(vertexOutput i):COLOR
            {
                i.normal = normalize(i.normal);
                //视角法线
                float3 viewdir = normalize(i.worldvertpos.xyz - _WorldSpaceCameraPos.xyz);// normalize(i.worldvertpos - _WorldSpaceCamerePos);
                float4 color = _AtmoColor;
                //视角法线与模型法线点积形成中间指为1向四周逐渐衰减为0的点积值，赋给透明通道，形成光晕效果
                color.a = pow(saturate(dot(viewdir, i.normal)), _OutLightPow);
                color.a *= _OutLightStrength*dot(viewdir, i.normal);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}