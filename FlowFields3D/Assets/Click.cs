using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	GameObject prt, buf;
	public GameObject[] lst;
	public Cube cubic;
	public ArrayList tags;
	public GameObject ter;
	public Table field;
	public Cell step;
	// Use this for initialization
	void Start () {
		prt = GameObject.FindGameObjectWithTag ("Particle");
		tags = new ArrayList();
		for (int i = 1; i <= 10; i++) {
			tags.Add(i);
		}
		ter = GameObject.FindGameObjectWithTag ("Terrain");
		field = ter.GetComponent<Table> ();
		buf = GameObject.FindGameObjectWithTag("Cub");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(2))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int mask = 1 << 8;
		if (Physics.Raycast(ray, out hit, 100, mask))
			{
				GameObject tmp = hit.collider.gameObject;
				prt.transform.position = tmp.transform.position;
				if((lst = GameObject.FindGameObjectsWithTag ("Current")).Length > 0)
				{
					lst = GameObject.FindGameObjectsWithTag ("Current");
					for (int i = 0; i < lst.Length - 1; i++)// ololo Bubble sort, cause fuck you, that's why!
					{
						for (int j = i + 1; j < lst.Length; j++)
						{
							if (lst[i].GetComponent<Cube>().ind > lst[j].GetComponent<Cube>().ind)
							{
								GameObject temp = lst[i];
								lst[i] = lst[j];
								lst[j] = temp;
							}
						}
					}
					Script.cords.Add(prt.transform.position);
					Script.cubes.Add(new ArrayList());
					foreach(GameObject lalka in lst)
					{
						lalka.tag = "Cub";
						cubic = lalka.GetComponent<Cube>();
						(Script.cubes[cubic.ind] as ArrayList).Remove(lalka);
						if((Script.cubes[cubic.ind] as ArrayList).Count < 1){
							Script.cubes.RemoveAt(cubic.ind);
							Script.cords.RemoveAt(cubic.ind);
							GameObject[] lst_buf;
							int max = cubic.ind;
							lst_buf = GameObject.FindGameObjectsWithTag("Current");
							foreach(GameObject lolka in lst_buf)
							{
								Cube tmps;
								tmps = lolka.GetComponent<Cube>();
								if(tmps.ind > cubic.ind)
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
								if(tmps.ind > cubic.ind)
								{
									if(tmps.ind > max) max = tmps.ind;
									tmps.ind--;
								}
							}
							if(!tags.Contains(max))tags.Add(max);
							tags.Sort();
						}
					}


	
					int cur_tag = (int)tags[0];
					tags.RemoveAt(0);
					foreach(GameObject lalka in lst)
					{
						cubic = lalka.GetComponent<Cube>();
						cubic.ind = cur_tag;
						(Script.cubes[cubic.ind] as ArrayList).Add(lalka);
					}
					foreach(GameObject lalka in lst)
					{
						lalka.transform.constantForce.force = new Vector3(10,0,0); // kick lazy cube's ass :3
					}
				}

			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			lst = GameObject.FindGameObjectsWithTag ("Current");
			if(lst.Length > 0){
				foreach(GameObject tmt in lst)
				{
					tmt.tag = "Cub";
				}
			}
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
