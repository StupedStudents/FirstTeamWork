using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	GameObject prt, buf, lst;
	// Use this for initialization
	void Start () {
		prt = GameObject.FindGameObjectWithTag ("Particle");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(2))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(buf != null) buf.tag = "Cub";
			int mask = 1 << 8;
			if (Physics.Raycast(ray, out hit, 100, mask))
			{
				GameObject tmp = hit.collider.gameObject;
				prt.transform.position = tmp.transform.position;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			lst = GameObject.FindGameObjectWithTag ("Current");
			if(lst != null) lst.tag = "Cub";
			int mask = 1 << 9;
			if (Physics.Raycast(ray, out hit, 100, mask))
			{
				GameObject tmp = hit.collider.gameObject;
				if(!tmp.collider.isTrigger){
					buf = tmp.gameObject;
					buf.tag = "Current";
				}
			}
		}
	}
}
