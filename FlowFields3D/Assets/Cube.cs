using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public int ind = 0;
	public Cell force;
	public Script ter;
	public float inTime = 0, times = 0;
	public Vector3 cur = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
		force = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Cell>();
		ter = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Script>();
		cur = this.transform.position;
		cur.y = 1;
	}

	// Update is called once per frame
	void Update () {
		if(this.name != "Eqv")
		{
			if(this.tag == "Current")
			{
				this.transform.Find ("Anim").renderer.enabled = true;
			}
			else this.transform.Find ("Anim").renderer.enabled = false;
		}
	}

	void FixedUpdate()
	{

		if((float)Time.realtimeSinceStartup - times > 10 && this.tag == "Current")
		{
			times = Time.realtimeSinceStartup;
			cur = this.transform.position;
		}
		if(ind != 0)
		{
			this.transform.constantForce.force = force.calcInfluence(ind,this.transform.position);

			if(Vector3.Distance((Vector3)(ter.cords[ind]),this.transform.position) > 5f)
			{		
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				                      Quaternion.LookRotation((Vector3)(ter.cords[ind]) 
				                        - this.transform.position), 3 * Time.deltaTime);
			}
	

		}
		else
		{
			float dist = Mathf.Sqrt (Mathf.Pow ((cur.x - this.transform.position.x), 2)
			                           + Mathf.Pow ((cur.z - this.transform.position.z), 2));
			Vector3 forc = new Vector3(0,0,0);
			if(dist > 1f)
			{
				forc = new Vector3(cur.x - this.transform.position.x ,0
				                   ,cur.z - this.transform.position.z);
				if (Mathf.Abs(forc.x) > 10F) {
					forc.x = 10F * Mathf.Sign(forc.x);
				}
				if (Mathf.Abs(forc.z) > 5F) {
					forc.z = 10F * Mathf.Sign(forc.z);
				}
			}
			this.transform.constantForce.force = forc ;
		}


	}
}
