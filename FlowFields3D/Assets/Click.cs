using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	GameObject prt, buf, lst;
	public Cube cubic;
	public Stack tags = new Stack();
	public GameObject ter;
	public Table field;
	public Cell step;
	// Use this for initialization
	void Start () {
		prt = GameObject.FindGameObjectWithTag ("Particle");
		for (int i = 10; i > 0; i--) {
			tags.Push (i);
		}
		ter = GameObject.FindGameObjectWithTag ("Terrain");
		field = ter.GetComponent<Table> ();
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
				if(buf != null){
					Script.cords.Add(prt.transform.position);
					Script.cubes.Add(new ArrayList());
					cubic = buf.GetComponent<Cube>();
					(Script.cubes[cubic.ind] as ArrayList).Remove(buf);

					if(cubic.ind != 0){
						Script.cubes.RemoveAt(cubic.ind);
						Script.cubes.Insert(cubic.ind, Script.cubes[Script.cubes.Count - 1]);
						Script.cords.RemoveAt(cubic.ind);
						Script.cords.Insert(cubic.ind + 1, Script.cords[Script.cords.Count - 1]);
					}

					if((Script.cubes[cubic.ind] as ArrayList).Count <= 0){
						Script.cubes.RemoveAt(cubic.ind);
						Script.cords.RemoveAt(cubic.ind);
						tags.Push (cubic.ind);
					}
		
					cubic.ind = (int)tags.Pop();
					(Script.cubes[cubic.ind] as ArrayList).Add(buf);
					for (int i = 0; i < 100; i++) {
						for (int j = 0; j < 100; j++) {
							step = field.table[i,j].GetComponent<Cell>();
							step.calcInfluence(cubic.ind);
						}
					}
					buf.transform.constantForce.force = new Vector3(10,0,0); // kick lazy cube's ass :3

				}
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
