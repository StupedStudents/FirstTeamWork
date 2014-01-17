using UnityEngine;
using System.Collections;

public class StaticField : MonoBehaviour {

	public Vector3 buf;
	public Vector3 randpost;

	void Start () {
		buf = new Vector3(0,0,0);
		randpost = new Vector3(0,0,0);
	}
		
	void OnTriggerStay(Collider col)
	{
		if((col.tag == "Cub" || col.tag == "Current") && Script.borders)
		{

			buf = -1 * new Vector3(this.transform.position.x
               - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.position).x ,0
               ,this.transform.position.z
               - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.position).z);
			Vector3 force = col.transform.constantForce.force;

			buf *= 0.03f;
			buf.x = 1f / buf.x;
			buf.z = 1f / buf.z;
			if (Mathf.Abs(buf.x) > 10F) {
				buf.x = 10f * Mathf.Sign(buf.x);
			}
			if (Mathf.Abs(buf.z)  > 10F) {
				buf.z = 10f * Mathf.Sign(buf.z); 
			}

			randpost = force.normalized;
			randpost.x = -Mathf.Sin(Mathf.PI/3f) * force.normalized.z + Mathf.Cos(Mathf.PI/3f) * force.normalized.x; 
			randpost.z = Mathf.Cos (Mathf.PI/3f) * force.normalized.z + Mathf.Sin(Mathf.PI/3f) * force.normalized.x;

			col.transform.constantForce.force += buf * 1.5f + 25 * Vector3.Reflect (force.normalized, randpost);
		}
	}

}
