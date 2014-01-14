using UnityEngine;
using System.Collections;

public class SphereTrigger : MonoBehaviour {

	public Vector3 buf = new Vector3(0,0,0);
	public Vector3 norm = new Vector3(0,0,0);
	public Vector3 norms = new Vector3(0,0,0);
	public Cell Super_cell;
	void Start () {
		Super_cell = GameObject.Find("Terrain").GetComponent<Cell>();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Cub" || col.tag == "Current")
		{
			Ray ray = new Ray();
			ray.direction = this.transform.parent.transform.constantForce.force;
			norms = ray.direction;
			Vector3 tmp1 = this.transform.parent.transform.position;
			tmp1.y += 0.5f;
			ray.origin = tmp1;
			norm = new Vector3(0,0,0);
			
			
			RaycastHit hit = new RaycastHit();
			if(ray.direction != new Vector3(0,0,0))
			{
				Debug.DrawRay(ray.origin, this.transform.parent.transform.constantForce.force, Color.blue, 1, false);
				if (col.GetComponent<BoxCollider>().Raycast(ray, out hit, 50)) {
					Vector3 reflectVec = Vector3.Reflect(tmp1, hit.normal);
					Debug.DrawLine(tmp1, hit.point, Color.red, 1, false);
					Debug.DrawRay(hit.point, reflectVec, Color.green,1, false);
					norm = hit.normal;
				}
			}
			 
			Cube tmp = this.transform.parent.transform.GetComponent<Cube>();
			Vector3 force = Super_cell.calcInfluence(tmp.ind,this.transform.position);

			this.transform.parent.transform.constantForce.force = force;

			
			if(norm != new Vector3(0,0,0))
			{
				this.transform.parent.transform.constantForce.force = Vector3.Reflect(this.transform.parent.transform.constantForce.force,norm);
				Debug.DrawRay(this.transform.position, this.transform.parent.transform.constantForce.force, Color.yellow);
			}

		}


	}

	void OnTriggerStay(Collider col)
	{
		float time = Time.realtimeSinceStartup;
		if(col.tag == "Cub" || col.tag == "Current")
		{
			float x = 0;

			x = Time.realtimeSinceStartup - time;
			if(norm != new Vector3(0,0,0))
			{
				this.transform.parent.transform.constantForce.force = Vector3.Reflect(this.transform.parent.transform.constantForce.force,norm);
				Debug.DrawRay(this.transform.position, this.transform.parent.transform.constantForce.force, Color.yellow);
			}

			Vector3 force = this.transform.parent.transform.constantForce.force;
			//buf = new Vector3(this.transform.parent.position.x - col.transform.position.x ,0
			  //                 ,this.transform.parent.position.z - col.transform.position.z);
			buf = new Vector3(this.transform.parent.position.x
			                  - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.parent.transform.position).x ,0
			                  ,this.transform.parent.position.z
			                  - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.parent.transform.position).z);
			buf *= 0.01f;
			buf.x = 1f / buf.x;
			buf.z = 1f / buf.z;
			if (Mathf.Abs(buf.x) > 10F) {
				buf.x = 10f * Mathf.Sign(buf.x);
			}
			if (Mathf.Abs(buf.z)  > 10F) {
				buf.z = 10f * Mathf.Sign(buf.z); 
			}
			force.x += x;
			force.z += x;
			this.transform.parent.transform.constantForce.force = force;
			this.transform.parent.transform.constantForce.force += buf * 1.7f + new Vector3(Random.value*10F - 5F,0,Random.value*10F - 5F);
			/*Ray ray = new Ray();
			ray.direction = this.transform.parent.transform.constantForce.force.normalized;
			norms = ray.direction;
			Vector3 tmp1 = col.transform.position;
			tmp1.y += 1;
			ray.origin = tmp1;


			RaycastHit hit = new RaycastHit();
			if(ray.direction != new Vector3(0,0,0))
			{
				Debug.DrawRay(ray.origin, this.transform.parent.transform.constantForce.force, Color.blue, 1, false);
				if (col.GetComponent<BoxCollider>().Raycast(ray, out hit, 50)) {
					Vector3 reflectVec = Vector3.Reflect(tmp1, hit.normal);
					Debug.DrawLine(tmp1, hit.point, Color.red);
					Debug.DrawRay(hit.point, reflectVec, Color.green);
					norm = hit.normal;
				}
			}*/



			/*Cube tmp = this.transform.parent.transform.GetComponent<Cube>();
			Vector3 force = Super_cell.calcInfluence(tmp.ind,this.transform.position);
			buf =  new Vector3(this.transform.parent.position.x - col.transform.position.x ,0
			                   ,this.transform.parent.position.z - col.transform.position.z);
			buf.x = 2f / buf.x;
			buf.z = 3f / buf.z;
			if (Mathf.Abs(buf.x) * 2F > 10F) {
				buf.x = 10f/2F * Mathf.Sign(buf.x);
			}
			if (Mathf.Abs(buf.z) * 3f > 10F) {
				buf.z = 10f/3F * Mathf.Sign(buf.z); 
			}
			this.transform.parent.transform.constantForce.force = force;
			this.transform.parent.transform.constantForce.force += buf * 10f + new Vector3(Random.value*10F - 5F,0,Random.value*10F - 5F);
			
			if(norm != new Vector3(0,0,0))
			{
				this.transform.parent.transform.constantForce.force = Vector3.Reflect(this.transform.parent.transform.constantForce.force,norm);
				Debug.DrawRay(this.transform.position, this.transform.parent.transform.constantForce.force, Color.yellow);
			}

			/*Cube tmp = col.transform.GetComponent<Cube>();
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

			if(norm != new Vector3(0,0,0))
			{
				col.constantForce.force = Vector3.Reflect(col.constantForce.force,norm);
			}*/
		}
	}
	void Update () {
		Debug.DrawRay(this.transform.position, this.transform.parent.transform.constantForce.force, Color.black);

	}

	void FixedUpdate(){
					/*	if (this.transform.parent.GetComponent<Cube> ().move)
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = false;
						else
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = true;*/
	}
}
