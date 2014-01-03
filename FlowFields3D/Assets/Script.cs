using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(35,2,35);
	float dist;

	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
	}

	public static void outDist(GameObject tmp){
		float distR;
		foreach (GameObject st in Script.cub) {
			distR = Mathf.Sqrt (Mathf.Pow (st.transform.position.x - tmp.transform.position.x, 2) +
			                           Mathf.Pow (st.transform.position.z - tmp.transform.position.z, 2));
			if(distR > 10 || st == tmp) {
				continue;
			}
			else {
				st.transform.constantForce.force = new Vector3(0,100,0);
				tmp.transform.constantForce.force = new Vector3(0,100,0);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
