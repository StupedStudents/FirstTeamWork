using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public  ArrayList cords = new ArrayList();
	public  ArrayList cubes = new ArrayList();
	public  ArrayList points = new ArrayList();
	public  GameObject[] cub;
	public  Vector3 finish  = new Vector3(0,0,0);

	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
		points.Add(GameObject.FindGameObjectWithTag("Particle"));
	}
	void Update () {

	}
}
