using UnityEngine;
using System.Collections;

public class SphereTrigger : MonoBehaviour {

	public Vector3 buf = new Vector3(0,0,0);
	public Vector3 norm = new Vector3(0,0,0);
	public Cell Super_cell;
	void Start () {
		Super_cell = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Cell>();
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == "Cub" || coll.tag == "Current")
		{


			Ray ray = new Ray();
			ray.direction = coll.transform.constantForce.force.normalized;
			if(ray.direction == new Vector3(0,0,0))
			{
				norm = new Vector3(0,0,0);
				return;
			}
			ray.origin = coll.transform.position;
			RaycastHit hit = new RaycastHit();
			this.GetComponent<CapsuleCollider>().Raycast(ray, out  hit, 20);
			coll.transform.FindChild("Sphere").GetComponent<SphereTrigger>().norm = hit.normal;
		}

	}

	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Cub" || col.tag == "Current")
		{
			Ray ray = new Ray();
			ray.direction = col.transform.position.normalized;
			if(ray.direction == new Vector3(0,0,0))
			{
				norm = new Vector3(0,0,0);
				return;
			}
			ray.origin = col.transform.position;
			RaycastHit hit = new RaycastHit();
			this.GetComponent<CapsuleCollider>().Raycast(ray, out  hit, 20);
			col.transform.FindChild("Sphere").GetComponent<SphereTrigger>().norm = hit.normal;
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
			col.transform.constantForce.force += buf * 10f + new Vector3(Random.value*10F - 5F,0,Random.value*10F - 5F);

			if(col.transform.FindChild("Sphere").GetComponent<SphereTrigger>().norm != new Vector3(0,0,0))
			{
				col.constantForce.force = Vector3.Reflect(col.constantForce.force,col.transform.FindChild("Sphere").GetComponent<SphereTrigger>().norm);
			}



		}
	}
	void Update () {

	}

	void FixedUpdate(){
						if (this.transform.parent.GetComponent<Cube> ().move)
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = false;
						else
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = true;
	}
}
