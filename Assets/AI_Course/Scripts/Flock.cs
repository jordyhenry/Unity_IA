using UnityEngine;

public class Flock : MonoBehaviour 
{
	public FlockManager m_manager;
	float speed;
	public bool turning = false;
	Animator anim;

	private void Start() 
	{
		speed = Random.Range(m_manager.minSpeed, m_manager.maxSpeed);
		anim = this.GetComponent<Animator>();
		anim.SetFloat("offset",Random.Range(0.0f,1.0f));
		anim.SetFloat("speed",speed);
	}

	private void Update() 
	{	
		Bounds b = new Bounds(m_manager.transform.position, m_manager.swimLimits*2);
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray(transform.position, transform.forward);

		Vector3 direction = m_manager.goalPos - transform.position;

		if(!b.Contains(transform.position))
			turning = true;
		else if (Physics.Raycast(ray, out hit, 50f))
		{
			turning = true;
			direction = Vector3.Reflect(transform.forward, hit.normal);
		}
		else
			turning = false;

		if(turning){
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(direction),
				m_manager.rotationSpeed * Time.deltaTime
			);
		}else{
			if(Random.Range(0,100) < 10)
				speed = Random.Range(m_manager.minSpeed, m_manager.maxSpeed);

			if(Random.Range(0, 100) < 20)
				ApplyRules();
		}

		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		anim.SetFloat("speed",speed);
	}

	void ApplyRules()
	{
		GameObject[] gos;
		gos = m_manager.allFish;

		Vector3 vCenter = Vector3.zero;
		Vector3 vAvoid = Vector3.zero;
		float gSpeed = 0.01f;
		float nDistance;
		int groupSize = 0;

		foreach (GameObject go in gos)
		{
			if(go != transform.gameObject)
			{
				nDistance = Vector3.Distance(go.transform.position, transform.position);
				if(nDistance <= m_manager.neighbourDistance)
				{
					vCenter += go.transform.position;
					groupSize++;

					if(nDistance < 1.0f)
					{
						vAvoid += (transform.position - go.transform.position);
					}

					Flock otherFlock = go.GetComponent<Flock>();
					gSpeed += otherFlock.speed;
				}
			}
		}

		if(groupSize > 0)
		{
			vCenter = vCenter/groupSize + (m_manager.goalPos - transform.position);
			speed = gSpeed/groupSize;

			Vector3 direction = (vCenter + vAvoid) - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(
					transform.rotation,
					Quaternion.LookRotation(direction),
					m_manager.rotationSpeed * Time.deltaTime
				);
		}
	}
}
