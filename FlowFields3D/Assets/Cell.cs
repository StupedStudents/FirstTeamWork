using UnityEngine;
using System.Collections;


public class Cell : MonoBehaviour {

	public ArrayList forces = new ArrayList();
	public float dist;

	public void distansce(int ind){
		dist = Mathf.Sqrt (Mathf.Pow (((Vector3)(Script.cords[ind])).x - this.transform.position.x, 2) + Mathf.Pow (((Vector3)(Script.cords[ind])).z - this.transform.position.z, 2));
	}
	public Vector3 magnitude(int ind){
		Vector3 force = new Vector3 (Mathf.Abs((((Vector3)(Script.cords[ind])).x - this.transform.position.x)) * ((((Vector3)(Script.cords[ind])).x - this.transform.position.x) / dist), 0,
		                             Mathf.Abs((((Vector3)(Script.cords[ind])).z - this.transform.position.z)) * ((((Vector3)(Script.cords[ind])).z - this.transform.position.z) / dist));
		
		if (Mathf.Abs(force.x) * 2F > 10F) {
			force.x = 10F * Mathf.Sign(force.x);
		}
		if (Mathf.Abs(force.z) * 2f > 10F) {
			force.z = 10F * Mathf.Sign(force.z);
		}
		return force;
	}

	void Start () {

		distansce (0);
		if (dist != 0) {
			forces.Add(magnitude (0));
		}
		else forces.Add(new Vector3(0,0,0));
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Cub")
		{
			col.constantForce.force = (Vector3)forces[0];
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
