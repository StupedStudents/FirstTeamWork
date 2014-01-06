using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static ArrayList cords = new ArrayList();
	public static ArrayList cubes = new ArrayList();
	public static ArrayList points = new ArrayList();
	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(0,0,0);

	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
		points.Add(GameObject.FindGameObjectWithTag("Particle"));
	}
	// Update is called once per frame
	void Update () {

	}
}
