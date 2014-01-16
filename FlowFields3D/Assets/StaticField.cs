using UnityEngine;
using System.Collections;

public class StaticField : MonoBehaviour {

	public Vector3 buf;

	void Start () {
		buf = new Vector3(0,0,0);
	}
	
	void OnTriggerStay(Collider col)
	{
		if((col.tag == "Cub" || col.tag == "Current") && Script.borders)
		{

			buf = -1 * new Vector3(this.transform.position.x
               - col.GetComponent<BoxCollider>().ClosestPointOnBounds(col.transform.position).x ,0
               ,this.transform.position.z
               - col.GetComponent<BoxCollider>().ClosestPointOnBounds(col.transform.position).z);
			Vector3 force = col.transform.constantForce.force;

			buf *= 0.01f;
			buf.x = 1f / buf.x;
			buf.z = 1f / buf.z;
			if (Mathf.Abs(buf.x) > 10F) {
				buf.x = 10f * Mathf.Sign(buf.x);
			}
			if (Mathf.Abs(buf.z)  > 10F) {
				buf.z = 10f * Mathf.Sign(buf.z); 
			}

			col.transform.constantForce.force = force;
			col.transform.constantForce.force += buf * 5f + new Vector3(Random.value*1000F - 500F,0,Random.value*1000F - 500F);
		}
	}

}
