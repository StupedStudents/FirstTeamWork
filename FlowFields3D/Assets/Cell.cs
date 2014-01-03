using UnityEngine;
using System.Collections;


public class Cell : MonoBehaviour {
	public Vector3 force = new Vector3(0,0,0);
	public float dist;
	// Use this for initialization
	void Start () {
		distansce ();
		if (dist != 0) {
			force = magnitude ();
		}
		else force = new Vector3(0,0,0);
	}

	void distansce(){
		dist = Mathf.Sqrt (Mathf.Pow (Script.finish.x - this.transform.position.x, 2) + Mathf.Pow (Script.finish.z - this.transform.position.z, 2));
	}
	Vector3 magnitude(){
		Vector3 force = new Vector3 (Mathf.Abs((Script.finish.x - this.transform.position.x)) * ((Script.finish.x - this.transform.position.x) / dist), 0,
		                             Mathf.Abs((Script.finish.z - this.transform.position.z)) * ((Script.finish.z - this.transform.position.z) / dist));
		
		if (Mathf.Abs(force.x) * 2F > 10F) {
			force.x = 10F * Mathf.Sign(force.x);
		}
		if (Mathf.Abs(force.z) * 2f > 10F) {
			force.z = 10F * Mathf.Sign(force.z);
		}
		return force;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Cub")
		{
			col.constantForce.force = force;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
