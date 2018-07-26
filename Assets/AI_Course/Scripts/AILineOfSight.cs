using UnityEngine;

public class AILineOfSight : MonoBehaviour 
{
	public Transform player;

	float rotationSpeed = 2.0f;
	float speed = 2.0f;
	public float visDist = 20.0f;
	public float visAngle = 30.0f;
	public float shootDist = 5.0f;

	string state = "IDLE";
	Animator anim;

	private void Start() 
	{
		anim = GetComponent<Animator>();	
	}

	private void Update() 
	{
		Vector3 direction = player.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		if(direction.magnitude < visDist && angle < visAngle)
		{
			direction.y = 0;

			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(direction),
				Time.deltaTime * rotationSpeed
			);

			if(direction.magnitude > shootDist){
				if(!state.Equals("RUNNING")){
					state = "RUNNING";
					anim.SetTrigger("isRunning");
				}
			}else{
				if(!state.Equals("SHOOTING")){
					state = "SHOOTING";
					anim.SetTrigger("isShooting");
				}
			}
		}else{
			if(!state.Equals("IDLE")){
				state = "IDLE";
				anim.SetTrigger("isIdle");
			}
		}

		if(state.Equals("RUNNING"))
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
}
