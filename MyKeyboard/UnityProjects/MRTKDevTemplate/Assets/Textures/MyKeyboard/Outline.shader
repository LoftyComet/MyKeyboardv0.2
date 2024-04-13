Shader "Custom/Outline"
{
    Properties
    {
        _Color ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (0.0, 0.03)) = .005
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "RenderType"="Opaque"}
        Pass
        {
        Cull Front
        Material
        {
            Diffuse [_Color]
            Ambient [_Color]
            Emission [_Color]
        }
        Lighting Off
        SetTexture [_Dummy] { combine primary }
        }
    }
}