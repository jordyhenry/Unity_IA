using UnityEngine;

public class FollowGoalCircuit : MonoBehaviour {

	public float speed = 10f;
	public float rotSpeed= 2f;
	public Transform goal;

	private void LateUpdate() 
	{
		Vector3 lookAtGoal = new Vector3(
			goal.position.x,
			transform.position.y,
			goal.position.z
		);
		
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			Quaternion.LookRotation(direction),
			rotSpeed * Time.deltaTime
		);

		transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
