using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static ArrayList cords = new ArrayList();
	public static ArrayList cubes = new ArrayList();
	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(0,0,0);
	
	float dist;
	public static float alpha=10f;
	public static float eps=10f;
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
	}
	// Update is called once per frame
	void Update () {

	}
}
