using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	GameObject[] cub = new GameObject[10];
	// Use this for initialization
	Vector3 finish  = new Vector3(35,2,35);
	Vector3 start = new Vector3(0,2,0);
	float dist;
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cub [0].transform.position = start;
		
	}

	void distansce(){
		dist = Mathf.Sqrt (Mathf.Pow (finish.x - cub [0].transform.position.x, 2) + Mathf.Pow (finish.z - cub [0].transform.position.z, 2));
	}
	Vector3 magnitude(){
		Vector3 force = new Vector3 (Mathf.Abs((finish.x - cub [0].transform.position.x)) * ((finish.x - cub [0].transform.position.x) / dist), 0,
		                             Mathf.Abs((finish.z - cub [0].transform.position.z)) * ((finish.z - cub [0].transform.position.z) / dist));
		return force;
	}
	// Update is called once per frame
	void Update () {
		distansce ();
		cub[0].constantForce.force = magnitude ();
		//cub[0].constantForce.force = new Vector3(magnitude ().x * 0.1F , 0, magnitude ().z * 0.1F);
	}
}
