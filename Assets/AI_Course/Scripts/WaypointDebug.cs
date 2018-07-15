using UnityEngine;


public class WaypointDebug : MonoBehaviour {
	void OnValidate () 
	{
		this.GetComponent<TextMesh>().text = this.transform.parent.gameObject.name;

		Vector3 pos = Camera.main.transform.position;
		Vector3 targetPostition = new Vector3( pos.x, transform.position.y, pos.z);
 		transform.LookAt( targetPostition ) ;
		transform.Rotate(0f, 180f, 0f);	
	}
}
