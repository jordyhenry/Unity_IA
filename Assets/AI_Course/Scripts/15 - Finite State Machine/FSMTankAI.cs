using UnityEngine;

public class FSMTankAI : MonoBehaviour 
{
	public GameObject player;
	public GameObject bulletPrefab;
	public GameObject turret;
	Animator anim;

	private void Start() 
	{
		anim = GetComponent<Animator>();	
	}

	private void Update() 
	{
		anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
		anim.SetFloat("angle", Vector3.Angle(transform.forward, (player.transform.position - transform.position)));
	}

	void Fire()
	{
		GameObject go = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
		go.GetComponent<Rigidbody>().AddForce(turret.transform.forward*500);
	}

	public void StartFiring()
	{
		InvokeRepeating("Fire", .5f, .5f);
	}

	public void StopFiring()
	{
		CancelInvoke("Fire");
	}
}
