﻿using UnityEngine;
using System.Collections;

public class SphereTrigger : MonoBehaviour {

	public Vector3 buf = new Vector3(0,0,0);
	public Vector3 norm = new Vector3(0,0,0);
	public Vector3 norms = new Vector3(0,0,0);
	public Cell Super_cell;
	public Vector3 randpost = new Vector3 (0,0,0);
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
			buf = new Vector3(this.transform.parent.position.x
			                  - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.parent.transform.position).x ,0
			                  ,this.transform.parent.position.z
			                  - col.GetComponent<BoxCollider>().ClosestPointOnBounds(this.transform.parent.transform.position).z);
			buf *= Script.epsilon;
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

			randpost = force.normalized;
			randpost.x = -Mathf.Sin(Mathf.PI/3f) * force.normalized.z + Mathf.Cos(Mathf.PI/3f) * force.normalized.x; 
			randpost.z = Mathf.Cos (Mathf.PI/3f) * force.normalized.z + Mathf.Sin(Mathf.PI/3f) * force.normalized.x;
			
			//col.transform.constantForce.force += buf * 1.5f + 25 * Vector3.Reflect (force.normalized, randpost);

			this.transform.parent.transform.constantForce.force = force;
			this.transform.parent.transform.constantForce.force += buf * Script.alpha;

			if(this.transform.parent.GetComponent<Cube>().move)
			{
				if(AngleAroundAxis(randpost,force.normalized,Vector3.up) > 0)
					col.transform.constantForce.force += 25 * Vector3.Reflect (force.normalized, randpost);
				else
					col.transform.constantForce.force += -25 * Vector3.Reflect (force.normalized, randpost);
			}
		}
	}
	// The angle between dirA and dirB around axis
	public float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis) {
		// Project A and B onto the plane orthogonal target axis
		dirA = dirA - Vector3.Project (dirA, axis);
		dirB = dirB - Vector3.Project (dirB, axis);
		
		// Find (positive) angle between A and B
		float angle = Vector3.Angle (dirA, dirB);
		
		// Return angle multiplied with 1 or -1
		return angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1);
	}

	void Update () {
		Debug.DrawRay(this.transform.position + new Vector3(0,0.5f,0), this.transform.parent.transform.constantForce.force, Color.yellow, 1f, false);
	}

	void FixedUpdate(){
		if (!Script.eachMoveble) {
						if (this.transform.parent.GetComponent<Cube> ().move)
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = false;
						else
								this.transform.parent.GetComponent<Cube> ().rigidbody.isKinematic = true;
				}
	}
}
