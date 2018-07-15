using UnityEngine;

public class PathfindAStart : MonoBehaviour 
{
	Transform goal;
	float speed = 10.0f;
	float accuracy = 1.0f;
	float rotSpeed = 4.0f;
	public GameObject wpManager;
	GameObject[] wps;
	GameObject currentNode;
	int currentWP = 0;
	Graph g;

	public GameObject startingPoint;
	public GameObject HelipadPoint;
	public GameObject RuinPoint;
	public GameObject FactoryPoint;

	private void Start() 
	{
		wps = wpManager.GetComponent<WPManager>().waypoints;
		g = wpManager.GetComponent<WPManager>().graph;
		currentNode = GetObjectFromGraph(startingPoint);
	}

	private void LateUpdate() 
	{
		if(g.getPathLength() == 0 || currentWP == g.getPathLength())
			return;

		//the node we are closest to at this moment
		currentNode = g.getPathPoint(currentWP);

		//if we are close enough to the current waypoint, move to next
		if(Vector3.Distance(
			g.getPathPoint(currentWP).transform.position,
			transform.position) < accuracy
		)
		{
			currentWP++;
		}

		if(currentWP < g.getPathLength())
		{
			goal = g.getPathPoint(currentWP).transform;
			Vector3 lookAtGoal = new Vector3(
				goal.position.x,
				transform.position.y,
				goal.position.z
			);

			Vector3 diretion = lookAtGoal - transform.position;

			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(diretion),
				Time.deltaTime * rotSpeed
			);

			transform.Translate(0, 0, speed * Time.deltaTime);
		}

	}

	public void GotoHeli()
	{
		g.AStar(currentNode, GetObjectFromGraph(HelipadPoint));
		currentWP = 0;
	}

	public void GotoRuin()
	{
		g.AStar(currentNode, GetObjectFromGraph(RuinPoint));
		currentWP = 0;
	}

	public void GotoFactory()
	{
		g.AStar(currentNode, GetObjectFromGraph(FactoryPoint));
		currentWP = 0;
	}

	GameObject GetObjectFromGraph(GameObject go)
	{
		for(int i=0; i <= wps.Length; i++)
			if(wps[i].Equals(go))
				return wps[i];

		Debug.LogError("Object isnt a valid waypoint !");
		return null;
	}
}
