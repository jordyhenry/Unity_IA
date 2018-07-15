using UnityEngine;

public class FlockManager : MonoBehaviour 
{
	public GameObject fishPrefab;
	public int numFish = 20;
	public GameObject[] allFish;
	public Vector3 swimLimits = new Vector3(5, 5, 5);
	public Vector3 goalPos;
	
	[Header("Fish Settings")]
	[Range(0f, 5f)]
	public float minSpeed;
	[Range(0f, 5f)]
	public float maxSpeed;
	[Range(1f, 10f)]
	public float neighbourDistance;
	[Range(0f, 5f)]
	public float rotationSpeed;

	private void Start() 
	{
		allFish = new GameObject[numFish];
		for (int i = 0; i < numFish; i++)
		{
			Vector3 pos = transform.position;
			pos.x += Random.Range(-swimLimits.x, swimLimits.x);
			pos.y += Random.Range(-swimLimits.y, swimLimits.y);
			pos.z += Random.Range(-swimLimits.z, swimLimits.z);

			allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity, transform);
			allFish[i].GetComponent<Flock>().m_manager = this;
		}	
	}

	private void Update() 
	{
		goalPos = transform.position;	
	}
}
