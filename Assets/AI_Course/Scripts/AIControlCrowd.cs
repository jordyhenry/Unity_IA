using UnityEngine;
using UnityEngine.AI;

public class AIControlCrowd : MonoBehaviour 
{

	GameObject[] goalLocations;
	UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	float speedMult;
	float detectionRadius = 5;
	float fleeRadius = 10;

	void Start () 
	{
		goalLocations = GameObject.FindGameObjectsWithTag("goal");
		agent = this.GetComponent<NavMeshAgent>();
		agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
		anim = GetComponent<Animator>();
		anim.SetFloat("wOffset", Random.Range(0,1));
		ResetAgent();
	}
	
	private void Update() 
	{
		if(agent.remainingDistance < 1)	
		{
			ResetAgent();
			agent.SetDestination(goalLocations[Random.Range(0,goalLocations.Length)].transform.position);
		}
	}

	void ResetAgent()
	{
		speedMult = Random.Range(.5f, 1.2f);
		agent.speed = 2 * speedMult;
		agent.angularSpeed = 120;
		anim.SetFloat("speedMult", speedMult);
		anim.SetTrigger("isWalking");
		agent.ResetPath();
	}

	public void DetectNewObstacle(Vector3 position)
	{
		if(Vector3.Distance(transform.position, position) <= detectionRadius){
			Vector3 fleeDirection = (transform.position - position).normalized;
			Vector3 newGoal = transform.position + fleeDirection * fleeRadius;

			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(newGoal, path);

			if(path.status != NavMeshPathStatus.PathInvalid){
				agent.SetDestination(path.corners[path.corners.Length -1]);
				anim.SetTrigger("isRunning");
				agent.speed = 10;
				agent.angularSpeed = 500;
			}
		}
	}
}
