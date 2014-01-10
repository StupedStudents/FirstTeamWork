using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {
	GameObject prt;
	public Click tagd;
	void Start () {
		tagd = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Click>();

	}
	void OnTriggerStay(Collider col){
		if(!Script.cords.Contains(this.transform.position)) return;
		if ((col.tag == "Cub" || col.tag == "Current") && col.GetComponent<Cube>().ind == Script.cords.IndexOf (this.transform.position)) {
			Cube tmp = col.GetComponent<Cube> ();
			if ((float)Time.realtimeSinceStartup - tmp.inTime > this.GetComponent<SphereCollider>().radius) {
				prt = Script.points[tmp.ind] as GameObject;
				(Script.cubes[tmp.ind] as ArrayList).Remove(col.gameObject);
				if((Script.cubes[tmp.ind] as ArrayList).Count < 1){
				
					Script.cubes.RemoveAt(tmp.ind);
					Script.cords.RemoveAt(tmp.ind);

					Script.points.RemoveAt(tmp.ind);
					Destroy(prt);
					GameObject[] lst_buf;
					int max = col.GetComponent<Cube>().ind;
					lst_buf = GameObject.FindGameObjectsWithTag("Current");
					foreach(GameObject lolka in lst_buf)
					{
						Cube tmps;
						tmps = lolka.GetComponent<Cube>();
						if(tmps.ind > tmp.ind)
						{
							if(tmps.ind > max) max = tmps.ind;
							tmps.ind--;
						}
					}
					lst_buf = GameObject.FindGameObjectsWithTag("Cub");
					foreach(GameObject lolka in lst_buf)
					{
						Cube tmps;
						tmps = lolka.GetComponent<Cube>();
						if(tmps.ind > tmp.ind)
						{
							if(tmps.ind > max) max = tmps.ind;
							tmps.ind--;
						}
					}
					if(!tagd.tags.Contains(max)) tagd.tags.Add(max);
					tagd.tags.Sort();
				}
				tmp.ind = 0;
				col.GetComponent<Cube>().cur = col.transform.position;

				(Script.cubes[tmp.ind] as ArrayList).Add(col);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if ((col.tag == "Cub" || col.tag == "Current") && col.GetComponent<Cube>().ind == Script.cords.IndexOf (this.transform.position)) {
			Cube tmp = col.GetComponent<Cube>();
			tmp.inTime = Time.realtimeSinceStartup;
		}
	}
	void Update () {

	}
}
