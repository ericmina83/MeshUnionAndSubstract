// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/StencilSample" {

	Properties {
		_Outline ("Outline Length", Range(0.0, 1.0)) = 0.2
		_Diffuse("Diffuse", Range(0.0, 1.0)) = 0.3
		_Color ("Color", Color) = (0.8, 0.8, 0.8, 1.0)
		_SelectedColor ("Selected Color", Color) = (0.2, 0.2, 0.2, 1.0)
		_UnselectedColor ("Unselected Color", Color) = (0.2, 0.2, 0.2, 1.0)
		[IntRange]_SID("SID", Range(0, 255)) = 0
		[Toggle(USE_TRANSPARENT)] _Selected("Selected", Int) = 0
		[Toggle(USE_TRANSPARENT)] _Transparent("Transparent", Int) = 0
	}
	
	SubShader {
	
		Tags { 
			"RenderType"="Opaque"
		}
	
		LOD 200
	
		// render model
		Pass {
		
            Tags
            {
				"Queue"="Transparent+1"
            }
		
			Stencil {
				Ref 128
				Comp Equal
			}

			Blend SrcAlpha OneMinusSrcAlpha 
	
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
            #include "Lighting.cginc"

			float4 _Color;
			float _Diffuse;
			bool _Transparent;
			
			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float3 worldPos: TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
			};
			
			v2f vert(appdata i) {
				v2f o;
				
				o.pos = UnityObjectToClipPos(i.vertex);
				o.worldPos = mul(unity_ObjectToWorld, i.vertex);
				o.worldNormal = UnityObjectToWorldNormal(i.normal);
                // o.worldNormal = normalize(mul((float3x3)unity_WorldToObject, i.normal));

				return o;
			}
			
			half4 frag(v2f i) : COLOR {
				
                fixed4 ambient = unity_AmbientSky;
                fixed3 worldLightDir = UnityWorldSpaceLightDir(i.worldPos);
                fixed3 diffuse = _LightColor0.rgb * _Diffuse * saturate(dot(i.worldNormal, worldLightDir));
                fixed4 finalColor = fixed4(diffuse + ambient, 1.0) ;
				
				if(_Transparent != 0)
					_Color.a = 0.0f;

				return (0, 0, 0, 0);
			}
			
			ENDCG
		}
		
		// render outline
		Pass {
		
            Tags
            {
				"Queue"="Transparent"
            }

			
			Stencil {
				Ref 128
				Comp always
				Pass replace
			}
			
			Cull Off
			// ZWrite Off
			
			Blend SrcAlpha OneMinusSrcAlpha 
		
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			float _Outline;
			float4 _SelectedColor;
			float4 _UnselectedColor;
			int _Selected;

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
			};
			
			v2f vert(appdata v) {
				v2f o;
				float scaleSize = 1 + _Outline;
				float4x4 scaleMat = float4x4(scaleSize, 0, 0, 0,
										 0, scaleSize, 0, 0,
										 0, 0, scaleSize, 0,
										 0, 0, 0, 1);
				
				float4 vert = v.vertex;
				o.pos = UnityObjectToClipPos(mul(scaleMat, vert));
				// o.pos = UnityObjectToClipPos(vert);
				
				return o;
			}
			
			half4 frag(v2f i) : COLOR {
				if (_Selected)
					return _SelectedColor;
				return _UnselectedColor;
			}	
			
			ENDCG
		}

	} 
	
	FallBack "Diffuse"
}