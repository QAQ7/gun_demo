// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Screen2"
{
	Properties
	{
		[PerRendererData]_MainTex("Texture",2D)="white"{}
		_Color("Tint",Color)=(1,1,1,1)

		_StencilComp("Stencil Comparison",Float)=8
		_Stencil("Stencil ID",Float)=0
		_StencilOp("Stencil Operation",Float)=8
		_StencilWriteMask("Stencil Write Mask",Float)=255
		_StencilReadMask("Stencil Read Mask",Float)=255

		_ColorMask("Color Mask",Float)=15
		_NoiseTex("Alpha Texture",2D)="white"{}
		_NoiseTex2("Alpha Texture",2D)="white"{}

		_DistortionFreq("distortionFreq",Float)=5
		_BKColor("BKColor",Color)=(0.5,0.5,0.5,1)

		_Scale("Scale",Float)=256
	}
	SubShader
	{

		Tags
		{
			"Queue"="Transparent"
			"IngoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		// No culling or depth
		Cull Off
		Lighting off
		ZWrite off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

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
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord0 : TEXCOORD0;
				half2 texcoord1 : TEXCOORD1;
			};

			fixed4 _Color;

			v2f vert (appdata IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord0 = IN.texcoord;
				OUT.texcoord1 = half2(IN.texcoord.x+_CosTime.y*0.25,IN.texcoord.y+frac(_Time.y));
				OUT.vertex.xy +=(_ScreenParams.zw-1.0)*float2(-1,1);
				OUT.color = IN.color*_Color;
				return OUT;
			}
			
			sampler2D _MainTex;
			sampler2D _NosieTex;
			sampler2D _NosieTex2;
			float _DistortionFreq;
			float _DistortionRoll;
			half4 _BKColor;

			fixed4 frag (v2f IN) : SV_Target
			{
				half2 tex0 = IN.texcoord0;
				half2 tex1 = IN.texcoord1;

				half dis = frac(tex0.y*_DistortionFreq + 0.5*_Time.y);
				dis *= (1-dis);
				dis /= 1+_CosTime.y*200*abs(text0.y);
				text0.x += dis*(text2D(_NoiseTex2,tex1).r-0.5);

				half4 color = tex2D(_MainTex,tex0);
				float noise = tex2D(_NoiseTex,tex1*4).r-0.2;

				color.rgb = lerp(color.rgb,BKColor,1-color.a)*noise*3;
				color.a=1;

				return color;
			}
			ENDCG
		}
	}
}
