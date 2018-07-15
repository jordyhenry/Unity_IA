using UnityEngine;

public class MoveGoalObject : MonoBehaviour 
{
	public float speed=5f;
	void Update () 
	{
		float movZ = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float movX = -Input.GetAxis("Vertical") * speed * Time.deltaTime;

		transform.Translate(movX, 0, movZ);
	}
}
