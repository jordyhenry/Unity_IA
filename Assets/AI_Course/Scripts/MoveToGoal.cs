using UnityEngine;

public class MoveToGoal : MonoBehaviour 
{
	public float speed = 1f;
	public float nearDistanceToTarget = .2f;
	public Transform goal;

	private void LateUpdate() 
	{
		transform.LookAt(goal.position);
		Vector3 direction = (goal.position - transform.position);

		Debug.DrawRay(transform.position, direction, Color.red);
		if(direction.magnitude > nearDistanceToTarget)
			transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
	}
}
