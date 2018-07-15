using UnityEngine;

public class WaypointFollow : MonoBehaviour 
{
	public Transform[] wayPoints;
	int currentWaypoint=0;

	public float speed = 1f;
	public float rotSpeed = .5f;
	public float minDistanceFromWaypoint = .1f;

	private void LateUpdate() 
	{
		if(wayPoints.Length <= 0) return;

		Vector3 lookAtGoal = new Vector3(
			wayPoints[currentWaypoint].position.x,
			transform.position.y,
			wayPoints[currentWaypoint].position.z
		);
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			Quaternion.LookRotation(direction),
			rotSpeed * Time.deltaTime
		);

		if(direction.magnitude < minDistanceFromWaypoint){
			currentWaypoint++;
			if(currentWaypoint >= wayPoints.Length)
				currentWaypoint = 0;
		}

		transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
