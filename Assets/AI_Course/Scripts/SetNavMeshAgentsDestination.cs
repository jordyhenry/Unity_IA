using UnityEngine;
using UnityEngine.AI;

public class SetNavMeshAgentsDestination : MonoBehaviour 
{
	NavMeshAgent[] agents;
	public Transform m_target;

	private void Start() 
	{
		agents = GameObject.FindObjectsOfType<NavMeshAgent>();
	}

	private void Update() 
	{
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100f)){
				foreach (NavMeshAgent agent in agents)
					agent.SetDestination(hit.point);

				if(m_target != null)
					m_target.position = hit.point;
			}
		}	
	}
}
