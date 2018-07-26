using UnityEngine;
using System;
using System.Collections;

public class FadeInOut : GenericSingletonClass<FadeInOut> 
{
	public Texture2D fadeTexture;

	private Action OnFadeEnd = null;
	private int drawDepth = -1000;
	private float alpha = 0;
	private bool canFade = true;

	public override void Awake ()
	{
		base.Awake ();
		fadeTexture = new Texture2D (1, 1);
		Color[] colors = new Color[1] {Color.black};
		fadeTexture.SetPixels(colors);
		fadeTexture.Apply ();
	}

	void OnGUI()
	{
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
	}

	// dir = 0 - Fade Out, dir = 1 Fade In
	IEnumerator fade(int dir, float time = 0.5f, Action callbackAction=null)
	{
		if (canFade) {
			canFade = false;
			OnFadeEnd = callbackAction;

			float start = alpha;
			float t = 0;

			while (t < 1) {
				t += Time.deltaTime / time;
				float newAlpha = Mathf.Lerp (start, dir, t);
				alpha = newAlpha;
				yield return null;
			}

			if (OnFadeEnd != null)
				OnFadeEnd ();

			canFade = true;
			//OnFadeEnd = null;
		} else {
			print ("there's another fade executing");
			yield return null;
		}
	}

	public IEnumerator fadeInfadeOut(float time = 0.5f, Action callbackAction=null)
	{
		fadeIn (time, callbackAction);
		do { yield return null;	} while(!canFade);
		fadeOut (time, null);
		//OnFadeEnd = null;
	}
		
	public void fadeIn(float time=0.5f, Action callbackAction=null)
	{
		StartCoroutine (fade (1,time,callbackAction));
	}

	public void fadeOut(float time=0.5f, Action callbackAction=null)
	{
		StartCoroutine (fade (0,time,callbackAction));
	}
}
