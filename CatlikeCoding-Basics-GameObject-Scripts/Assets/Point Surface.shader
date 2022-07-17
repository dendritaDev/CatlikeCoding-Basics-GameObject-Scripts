Shader "Graph/Point Surface" {

	Properties{
		_Smoothness ("Smoothness", Range(0,1)) = 0.5
	}

SubShader {
		
	CGPROGRAM
	#pragma surface ConfigureSurface Standard fullforwardshadows
	#pragma target 3.0

	struct Input {
		float3 worldPos;
	};

float _Smoothness;

	void ConfigureSurface (Input input, inout SurfaceOutputStandard surface) {
		surface.Albedo.rg = saturate(input.worldPos.xy * 0.5 + 0.5); //esto lo hacemos porque como nuestros valores van de -1 a 1, pero en terminos de color, no existen colores numericamente negativos, tenemos sque hacer que empiece en 0 el rango. Solo lo hacemos para los colores red y green .rg
		surface.Smoothness = _Smoothness;
	}

	ENDCG
}
	
	FallBack "Diffuse"


}
