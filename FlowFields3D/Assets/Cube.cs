using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public int ind = 0;
	public Cell force;
	// Use this for initialization
	void Start () {
		force = new Cell();
	}

	// Update is called once per frame
	void Update () {
		this.transform.constantForce.force = force.calcInfluence(ind,this.transform.position) + Script.outDist (this.gameObject);
	}

	void FixedUpdate()
	{
	
	}
}
