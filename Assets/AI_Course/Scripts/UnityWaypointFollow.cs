using UnityEngine;

public class UnityWaypointFollow : MonoBehaviour 
{
	public UnityStandardAssets.Utility.WaypointCircuit m_wayPointCircuit;
	int currentWaypoint=0;

	public float speed = 1f;
	public float rotSpeed = .5f;
	public float minDistanceFromWaypoint = .1f;

	private void LateUpdate() 
	{
		if(m_wayPointCircuit.Waypoints.Length <= 0) return;

		Vector3 lookAtGoal = new Vector3(
			m_wayPointCircuit.Waypoints[currentWaypoint].position.x,
			transform.position.y,
			m_wayPointCircuit.Waypoints[currentWaypoint].position.z
		);
		Vector3 direction = lookAtGoal - transform.position;

		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			Quaternion.LookRotation(direction),
			rotSpeed * Time.deltaTime
		);

		if(direction.magnitude < minDistanceFromWaypoint){
			currentWaypoint++;
			if(currentWaypoint >= m_wayPointCircuit.Waypoints.Length)
				currentWaypoint = 0;
		}

		transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
