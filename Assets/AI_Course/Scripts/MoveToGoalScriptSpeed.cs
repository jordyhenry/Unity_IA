using UnityEngine;

public class MoveToGoalScriptSpeed : MonoBehaviour 
{
	public Transform goal;
	public float speed = .5f;
	public float rotSpeed = .5f;
	public float minDistanceToTarget=.1f;
	

	private void LateUpdate() 
	{
		Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

		if(Vector3.Distance(transform.position, lookAtGoal) > minDistanceToTarget)
			transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
