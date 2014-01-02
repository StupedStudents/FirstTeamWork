using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	GameObject[] cub = new GameObject[10];
	// Use this for initialization
	public static Vector3 finish  = new Vector3(35,2,35);
	public static Vector3 start = new Vector3(-35,2,-35);
	float dist;
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cub [0].transform.position = start;
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
