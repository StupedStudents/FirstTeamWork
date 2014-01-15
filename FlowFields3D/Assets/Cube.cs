using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public bool move = false;
	public int ind = 0;
	public Cell force;
	public Script ter;
	public float inTime = 0, times = 0, wait = 0;
	public Vector3 cur = new Vector3(0,0,0);

	void Start () {
		force = GameObject.Find("Terrain").GetComponent<Cell>();
		ter = GameObject.Find("Terrain").GetComponent<Script>();
		cur = this.transform.position;
		cur.y = 1;
	}

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
		if((float)Time.realtimeSinceStartup - wait > 0.04f)
		{

			if(this.constantForce.force != new Vector3(0,0,0) && this.move)
			{
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				                                           Quaternion.LookRotation(this.constantForce.force), 
				                                           Script.phi * Time.deltaTime);
			}

			wait = Time.realtimeSinceStartup;
			if((float)Time.realtimeSinceStartup - times > 10 && this.tag == "Current")
			{
				times = Time.realtimeSinceStartup;
				cur = this.transform.position;
			}
			if(ind != 0)
			{
				this.transform.constantForce.force = force.calcInfluence(ind,this.transform.position);
				
				if(Vector3.Distance((Vector3)(ter.cords[ind]),this.transform.position) > 10f)
				{		
					this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
					                                           Quaternion.LookRotation((Vector3)(ter.cords[ind]) 
					                        - this.transform.position), 5 * Time.deltaTime);
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
					if (Mathf.Abs(forc.x) > 5F) {
						forc.x = 5F * Mathf.Sign(forc.x);
					}
					if (Mathf.Abs(forc.z) > 5F) {
						forc.z = 5F * Mathf.Sign(forc.z);
					}
				}
				this.transform.constantForce.force = forc ;
			}
		}
	}
}
