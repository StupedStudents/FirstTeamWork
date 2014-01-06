using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay(Collider col){
		if ((col.tag == "Cub" || col.tag == "Current") && col.GetComponent<Cube>().ind == Script.cords.IndexOf (this.transform.position)) {
			Cube tmp = col.GetComponent<Cube> ();
			if ((float)Time.realtimeSinceStartup - tmp.inTime > 8) {
				tmp.ind =0;
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if ((col.tag == "Cub" || col.tag == "Current") && col.GetComponent<Cube>().ind == Script.cords.IndexOf (this.transform.position)) {
			Cube tmp = col.GetComponent<Cube>();
			tmp.inTime = Time.realtimeSinceStartup;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
