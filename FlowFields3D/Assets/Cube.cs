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

		//this.transform.constantForce.force = Script.outDist (this.gameObject);
	}

	void FixedUpdate()
	{
		this.transform.constantForce.force = ter.outDist (this.gameObject,ind);
	}
}
