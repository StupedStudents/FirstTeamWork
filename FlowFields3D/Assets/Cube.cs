using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public int ind = 0;
	public Cell force;
	public Script ter;
	public float inTime =0;
	// Use this for initialization
	void Start () {
		force = GameObject.FindGameObjectWithTag("Cell").GetComponent<Cell>();
		ter = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Script>();
	}

	// Update is called once per frame
	void Update () {
		if(this.name != "Eqv")
		{
			if(this.tag == "Current")
			{
				this.transform.GetChild(0).renderer.enabled = true;
			}
			else this.transform.GetChild(0).renderer.enabled = false;
		}

	}

	void FixedUpdate()
	{
		if(ind != 0)
		{
			this.transform.constantForce.force = force.calcInfluence(ind,this.transform.position);
		}
		else
		{
			this.transform.constantForce.force = new Vector3(0,0,0);
		}


	}
}
