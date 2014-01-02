using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	GameObject[] cub = new GameObject[10];
	// Use this for initialization
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cub[0].transform.position = new Vector3 (5, 5, 5);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
