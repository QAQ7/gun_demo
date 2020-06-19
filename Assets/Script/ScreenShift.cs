using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShift : MonoBehaviour {

	public Shader shader;
	public float shiftNum = 0.2f;
	public float deshiftNum = 0.01f;
	public Material material;
	//public Material _material;
	/*private Material material
	{
		get
		{
			if (shader == null)
				return null;

			if (_material == null)
			{
				_material = new Material(shader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}

			return _material;
		}
	}*/

	void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}

		if (shader == null || !shader.isSupported)
		{
			enabled = false;
		}
	}

	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (material != null) {
			if (material.GetFloat ("scale") > 1) {
				material.SetFloat ("scale", material.GetFloat ("scale") - deshiftNum);
			} else {
				material.SetFloat ("scale", 1.0f);
			}
			Graphics.Blit (sourceTexture, destTexture, material);
		}else
			Graphics.Blit(sourceTexture, destTexture);
	}

	public void shift(){
		material.SetFloat("scale",material.GetFloat ("scale") + shiftNum);
		Debug.Log ("shift");
	}
	//void OnDisable()
	//{
	//	if (_material)
	//	{
	//		DestroyImmediate(_material);
	//	}
	//}
}