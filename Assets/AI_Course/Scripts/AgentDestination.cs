using UnityEngine;
using UnityEngine.AI;

public class AgentDestination : MonoBehaviour 
{
	NavMeshAgent agent;
	public Transform goal;

	private void Start() 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(goal.position);
	}
}
