using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static ArrayList cords = new ArrayList();
	public static ArrayList cubes = new ArrayList();
	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(35,2,35);
	
	float dist;
	public static float alpha=0.5f;
	public static float eps=0.6f;
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
	}

	public static void outDist(GameObject tmp){
		float distR;
		foreach (GameObject st in Script.cub) {
			distR = Mathf.Sqrt (Mathf.Pow (st.transform.position.x - tmp.transform.position.x, 2) +
			                           Mathf.Pow (st.transform.position.z - tmp.transform.position.z, 2));
			if(distR > 2 || st == tmp) {
				continue;
			}
			else {
				tmp.transform.constantForce.force = tmp.transform.constantForce.force + new Vector3(
					-1*(((st.transform.position.z - tmp.transform.position.z)/distR)*alpha)
					/
					(distR/((eps*alpha)+1)),
					0,
					(((st.transform.position.x - tmp.transform.position.x)/distR)*alpha)
					/
					(distR/((eps*alpha)+1)));
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
