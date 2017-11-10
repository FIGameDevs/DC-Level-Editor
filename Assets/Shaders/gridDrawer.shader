// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "LevelEditor/GridDrawer"
{
	Properties
	{
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		Cull Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

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
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float l = lerp(0.5 ,-10,abs((i.worldPos.x + 2) % 4));
				float r = lerp(0.5,-10,abs((i.worldPos.z + 2) % 4));
				float4 col = float4(l+r,l+r,l+r,1-l*r);
				/*
				if (abs((i.worldPos.x + 2) % 4) < 0.025 || abs((i.worldPos.z + 2) % 4) < 0.025){
					col = float4(1,1,1,1);
				}*/
				
				return col;
			}
			ENDCG
		}
	}
}
