using UnityEngine;

public class MoveInStraightLine : MonoBehaviour 
{
	public Vector3 goal = new Vector3(5,0,4);
	public float speed = .1f;

	void LateUpdate () 
	{
		transform.Translate(goal.normalized * speed * Time.deltaTime);
	}
}
