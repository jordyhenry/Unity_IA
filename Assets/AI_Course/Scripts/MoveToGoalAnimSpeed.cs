using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoalAnimSpeed : MonoBehaviour {

	public Transform goal;
	public float rotSpeed = 0.4f;

	void LateUpdate () 
	{
		Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);
	}
}
