using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAICar : MonoBehaviour {

	public Transform goal;
	public Text readout;
	public float acceleration = 5f;
	public float deceleration = 5f;
	public float minSpeed = 0f;
	public float maxSpeed = 100f;
	public float brakeAngle = 20f;
	public float rotSpeed = 10.0f;
	
	float speed = 0f;

	void LateUpdate () 
	{
		Vector3 lookAtGoal = new Vector3(goal.position.x, 
										transform.position.y, 
										goal.position.z);
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation, 
												Quaternion.LookRotation(direction), 
												Time.deltaTime*rotSpeed);

		if(Vector3.Angle(goal.forward, transform.forward) > brakeAngle && speed > maxSpeed)
			speed = Mathf.Clamp(speed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
		else	
			speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);

		transform.Translate(0,0,speed);
		AnalogueSpeedConverter.ShowSpeed(speed, minSpeed, maxSpeed);
		if(readout)
			readout.text = "" + (int)(speed * 200);
	}
}
