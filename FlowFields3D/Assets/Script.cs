using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static ArrayList cords = new ArrayList();
	public static ArrayList cubes = new ArrayList();
	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(0,0,0);
	
	float dist;
	public static float alpha=0.0005f;
	public static float eps=3f;
	void Start () {
		cub = GameObject.FindGameObjectsWithTag ("Cub");
		cords.Add (finish);
		cubes.Add (new ArrayList());
		foreach (GameObject t in cub) {
						(cubes [0] as ArrayList).Add (t);
				}
	}

	public static Vector3 outDist(GameObject tmp){
		float distR;
		Vector3 force = new Vector3(0,0,0);
		foreach (GameObject st in Script.cub) {
			distR = Mathf.Sqrt (Mathf.Pow (st.transform.position.x - tmp.transform.position.x, 2) +
			                           Mathf.Pow (st.transform.position.z - tmp.transform.position.z, 2));
			if(distR > 2 || st == tmp) {
				continue;
			}
			else {
				 force +=  tmp.transform.constantForce.force + new Vector3(
					(Mathf.Cos(3.14F/4F)*(alpha))
					/
					(distR/((eps*alpha))+1),
					0,
					(alpha)
					/
					(distR/((eps*alpha)))+1);
				if (Mathf.Abs(force.x) * 2F > 10F) {
					force.x = 10F * Mathf.Sign(force.x);
				}
				if (Mathf.Abs(force.z) * 2f > 10F) {
					force.z = 10F * Mathf.Sign(force.z);
				}
				return force;
			}

		}
		return force;
	}
	
	// Update is called once per frame
	void Update () {

		/*GameObject[] curr = GameObject.FindGameObjectsWithTag ("Current");
		if(curr.Length > 0){
			Animator an = curr[0].GetComponent<Animator>();
			an.Play();
		}*/
	}
}
