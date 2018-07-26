using UnityEngine;

public class MoveInStraightLine : MonoBehaviour 
{
	public Vector3 goal = new Vector3(5,0,4);
	public float speed = .1f;
	public bool isNormalized = true;

	void LateUpdate () 
	{
		if(isNormalized)
			transform.Translate(goal.normalized * speed * Time.deltaTime);
		else
			transform.Translate(goal * speed * Time.deltaTime);
	}
}
