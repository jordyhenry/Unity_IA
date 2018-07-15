using UnityEngine;

public class SmoothCameraFollowMultiple : MonoBehaviour 
{
	public Transform[] targets;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	private int index = 0;

	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Space))	
		{
			if(index + 1 >= targets.Length)
				index = 0;
			else
				index++;
		}
	}

	void FixedUpdate ()
	{
		Vector3 desiredPosition = targets[index].position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(targets[index]);
	}
}
