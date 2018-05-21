using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fade_texture;
	public float fade_speed = 1.0f;

	private float mDir = -1.0f;
	private float mAlpha = 1.0f;

	void OnGUI()
	{
		mAlpha = Mathf.Clamp01(mAlpha + mDir * fade_speed * Time.deltaTime);
		if (mAlpha <= 0.001f)
		{
			return;
		}

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, mAlpha);
		GUI.depth = -1000;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fade_texture);
	}

	public float BeginFade(float i_dir)
	{
		mDir = i_dir;
        return fade_speed;
	}

	void OnLevelWasLoaded()
	{
		BeginFade(-1.0f);
	}
}
