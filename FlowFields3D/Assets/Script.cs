using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public  ArrayList cords = new ArrayList();
	public  ArrayList cubes = new ArrayList();
	public  ArrayList points = new ArrayList();
	public  GameObject[] cub;
	public  Vector3 finish  = new Vector3(0,0,0);
	public static bool eachMoveble = false;
	public static bool borders = true;
	public static float alpha = 1.7f;
	public static float epsilon = 0.01f;
	public static float phi = 2f;
	public Click cl;


	void Start () {
		cl = GameObject.Find("Main Camera").GetComponent<Click>();
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
		points.Add(GameObject.FindGameObjectWithTag("Particle"));
	}
	void Update () {
		int min = (int)cl.tags[0];
			if(cords.Count > min) cords.RemoveAt(min);
			if(cubes.Count > min) cubes.RemoveAt(min);
			if(points.Count > min)
			{
				Destroy(points[min] as GameObject);
				points.RemoveAt(min);
			}
	}
}
