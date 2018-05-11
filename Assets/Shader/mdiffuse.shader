// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/mdiffuse" {
	Properties {

	}
	SubShader {
		pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			struct a2v{
				float4 vertex: POSITION;
			};

			struct v2f{
				float4 position: SV_POSITION; 
			};

			v2f vert(a2v v){
				v2f f;
				f.position = UnityObjectToClipPos(v.vertex);
				return f;
			};

			float4 frag(v2f f):SV_Target{
				return fixed4(1,1,1,1);
			};
			ENDCG

		}
	}
	FallBack "Diffuse"
}
