using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public int ind = 0;
	public Cell force;
	public Script ter;
	// Use this for initialization
	void Start () {
		force = GameObject.FindGameObjectWithTag("Cell").GetComponent<Cell>();
		ter = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Script>();

	}

	// Update is called once per frame
	void Update () {

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
