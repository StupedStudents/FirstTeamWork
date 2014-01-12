using UnityEngine;
using System.Collections;

public class SphereTrigger : MonoBehaviour {

	public Vector3 buf = new Vector3(0,0,0);
	public Vector3 coll = new Vector3(0,0,0);
	public Cell Super_cell;
	void Start () {
		Super_cell = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Cell>();
	}

	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Cub" || col.tag == "Current")
		{

			Cube tmp = col.transform.GetComponent<Cube>();
			Vector3 force = Super_cell.calcInfluence(tmp.ind,col.transform.position);
			buf = new Vector3(col.transform.position.x - this.transform.parent.position.x,0
			                  ,col.transform.position.z - this.transform.parent.position.z);

			buf.x = 2f / buf.x;
			buf.z = 3f / buf.z;
			if (Mathf.Abs(buf.x) * 2F > 10F) {
				buf.x = 10f/2F * Mathf.Sign(buf.x);
			}
			if (Mathf.Abs(buf.z) * 3f > 10F) {
				buf.z = 10f/3F * Mathf.Sign(buf.z); 
			}
			col.transform.constantForce.force = force;
			col.transform.constantForce.force += buf * 10f;
		}
	}
	void Update () {

	}
}
