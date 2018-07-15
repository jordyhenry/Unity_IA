using UnityEngine;

public class FleeController : MonoBehaviour 
{
	public Transform obstacle;
	AIControlCrowd[] agents;

	private void Start() 
	{
		agents = GameObject.FindObjectsOfType<AIControlCrowd>();
	}

	private void Update() 
	{
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100f)){
				obstacle.position = hit.point;
				foreach(AIControlCrowd a in agents)
					a.DetectNewObstacle(hit.point);
			}
		}
	}
}
