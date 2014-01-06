using UnityEngine;
using System.Collections;

public class Script : MonoBehaviour {

	public static ArrayList cords = new ArrayList();
	public static ArrayList cubes = new ArrayList();
	public static GameObject[] cub;
	public static Vector3 finish  = new Vector3(0,0,0);
	public Cell Super_cell;
	
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
		Super_cell = GameObject.FindGameObjectWithTag("Cell").GetComponent<Cell>();
	}

	public Vector3 outDist(GameObject tmp, int ind){
		float distR;
		Vector3 buf = new Vector3(0,0,0);
		Vector3 force = Super_cell.calcInfluence(ind,tmp.transform.position);
		foreach (GameObject st in Script.cub) {
			distR = Mathf.Sqrt (Mathf.Pow (st.transform.position.x - tmp.transform.position.x, 2) +
			                           Mathf.Pow (st.transform.position.z - tmp.transform.position.z, 2));
			if(distR > 2 || st == tmp) {
				continue;
			}
			else {
				/* force += new Vector3(
					(alpha)
					/
					(distR/((eps*alpha))+1),
					0,
					(alpha)
					/
					(distR/((eps*alpha))+1));*/

				buf = new Vector3(tmp.transform.position.x - st.transform.position.x,0
				                     ,tmp.transform.position.z - st.transform.position.z);
				buf.x = 2f/buf.x;
				buf.z = 2f/buf.z;
				if (Mathf.Abs(buf.x) * 2F > 10F) {
					buf.x = 10F * Mathf.Sign(buf.x);
				}
				if (Mathf.Abs(buf.z) * 2f > 10F) {
					buf.z = 10F * Mathf.Sign(buf.z);
				}
				force += buf * 5f;
				return force/2f;
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
