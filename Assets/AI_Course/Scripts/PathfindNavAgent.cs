using UnityEngine;
using UnityEngine.AI;

public class PathfindNavAgent : MonoBehaviour 
{

	public GameObject wpManager;
	GameObject[] wps;
	NavMeshAgent agent;

	public GameObject HelipadPoint;
	public GameObject RuinPoint;
	public GameObject FactoryPoint;

	private void Start() 
	{
		wps = wpManager.GetComponent<WPManager>().waypoints;
		agent = GetComponent<NavMeshAgent>();
	}

	public void GotoHeli()
	{
		agent.SetDestination(GetObjectFromGraph(HelipadPoint));
	}

	public void GotoRuin()
	{
		agent.SetDestination(GetObjectFromGraph(RuinPoint));
	}

	public void GotoFactory()
	{
		agent.SetDestination(GetObjectFromGraph(FactoryPoint));
	}

	Vector3 GetObjectFromGraph(GameObject go)
	{
		for(int i=0; i <= wps.Length; i++)
			if(wps[i].Equals(go))
				return wps[i].transform.position;

		Debug.LogError("Object isnt a valid waypoint !");
		return Vector3.zero;
	}
}
