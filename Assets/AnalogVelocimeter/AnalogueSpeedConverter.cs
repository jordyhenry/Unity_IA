using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalogueSpeedConverter : MonoBehaviour {

	static float minAngle = 581.0f;
	static float maxAngle = 313.0f;
	static AnalogueSpeedConverter thisSpeedo;

	void Start () {
		thisSpeedo = this;
	}
	
	public static void ShowSpeed(float speed, float min, float max)
	{
		float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(min, max, speed));
		thisSpeedo.transform.eulerAngles = new Vector3(0,0,ang);
	}
}
