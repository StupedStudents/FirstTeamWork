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
	public GameObject spr;
	public Script script;

	void Start () {
		tags = new ArrayList();
		for (int i = 1; i <= 10; i++) {
			tags.Add(i);
		}
		script = GameObject.Find("Terrain").GetComponent<Script>();
		ter = GameObject.Find ("Terrain");
		field = ter.GetComponent<Table> ();
		buf = GameObject.FindGameObjectWithTag("Cub");
		prt = new GameObject();
	}

	void Update () {
		if (Input.GetMouseButtonDown(2))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int mask = 1 << 0;
		if (Physics.Raycast(ray, out hit, 100, mask))
			{
				if((lst = GameObject.FindGameObjectsWithTag ("Current")).Length > 0)
				{
					script.points.Add(Instantiate((script.points[0] as GameObject),(hit.point),new Quaternion()) as GameObject);
					(script.points[(script.points.Count - 1)] as GameObject).transform.Rotate(new Vector3(270,0,0));
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
					script.cords.Add(hit.point);
					script.cubes.Add(new ArrayList());
					foreach(GameObject lalka in lst)
					{
						lalka.tag = "Cub";
						lalka.GetComponent<Cube>().move = true;

						cubic = lalka.GetComponent<Cube>();
						(script.cubes[cubic.ind] as ArrayList).Remove(lalka);
						if((script.cubes[cubic.ind] as ArrayList).Count < 1){
							script.cubes.RemoveAt(cubic.ind);
							script.cords.RemoveAt(cubic.ind);
							prt = script.points[cubic.ind] as GameObject;
							script.points.RemoveAt(cubic.ind);
							Destroy(prt);
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
						(script.cubes[cubic.ind] as ArrayList).Add(lalka);
					}
				}
				spr = (script.points[(script.points.Count - 1)] as GameObject).transform.Find ("Rad").gameObject;
				if(script.cords.Contains(spr.transform.position))
				{
					spr.GetComponent<SphereCollider>().radius =
						((script.cubes[script.cords.IndexOf (spr.transform.position)] as ArrayList).Count * 2 + 5);
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
