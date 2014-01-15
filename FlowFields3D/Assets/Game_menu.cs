using UnityEngine;
using System.Collections;

public class Game_menu : MonoBehaviour {

	public GameObject tiger_p;
	public GameObject tank_p;
	public GameObject truck_p;
	private bool ti = false, ta = false, tr = false;
	private Vector3 screenPos = new Vector3(0,0,0);
	private AreaSelect area;
	private Script script;
	GameObject tmp_tank, tmp_tiger, tmp_truck;

	// Use this for initialization
	void Start () {
		area = GameObject.Find ("Main Camera").GetComponent<AreaSelect>();
		script = GameObject.Find("Terrain").GetComponent<Script>();
		tmp_tiger = GameObject.Instantiate(tiger_p, screenPos, new Quaternion()) as GameObject;
		tmp_tiger.name = "Tiger_tmp";
		tmp_tiger.SetActive(false);


		tmp_tank = GameObject.Instantiate(tank_p, screenPos, new Quaternion()) as GameObject;
		tmp_tank.name = "Tank_tmp"; 
		tmp_tank.SetActive(false);

		tmp_truck = GameObject.Instantiate(truck_p, screenPos, new Quaternion()) as GameObject;
		tmp_truck.name = "Truck_tmp";
		tmp_truck.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(ti)
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray, out hit, 1000, 1 << 0)) { 
				if (hit.collider.tag == "Terrain") {
					screenPos = hit.point;
				} 
			}
			if (Input.GetMouseButtonDown (0)) {  
				GameObject buf = GameObject.Instantiate(tiger_p,screenPos,new Quaternion()) as GameObject;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().radius *= Script.epsilon * 100;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().height *= Script.epsilon * 100;
				(script.cubes [0] as ArrayList).Add (buf);
				area.units.Add(buf);
				ti = false;
				tmp_tiger.SetActive(false);

			}
			tmp_tiger.transform.position = screenPos;
		}

		if(tr)
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray, out hit, 1000, 1 << 0)) { 
				if (hit.collider.tag == "Terrain") {
					screenPos = hit.point;
				} 
			}

			if (Input.GetMouseButtonDown (0)) {  
				GameObject buf = GameObject.Instantiate(truck_p,screenPos,new Quaternion()) as GameObject;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().radius *= Script.epsilon * 100;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().height *= Script.epsilon * 100;
				(script.cubes [0] as ArrayList).Add (buf);
				area.units.Add(buf);
				tr = false;
				tmp_truck.SetActive(false);
			}
			tmp_truck.transform.position = screenPos;
		}

		if(ta)
		{
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray, out hit, 1000, 1 << 0)) { 
				if (hit.collider.tag == "Terrain") {
					screenPos = hit.point;
				} 
			}
	
			if (Input.GetMouseButtonDown (0)) {  
				GameObject buf = GameObject.Instantiate(tank_p,screenPos,new Quaternion()) as GameObject;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().radius *= Script.epsilon * 100;
				buf.transform.FindChild("Sphere").GetComponent<CapsuleCollider>().height *= Script.epsilon * 100;
				(script.cubes [0] as ArrayList).Add (buf);
				area.units.Add(buf);
				ta = false;
				tmp_tank.SetActive(false);

			}
			tmp_tank.transform.position = screenPos;
		}
	}

	void del()
	{
		GameObject[] kokoko = GameObject.FindGameObjectsWithTag("Current");
		for (int i = 0; i < kokoko.Length - 1; i++)
		{
			for (int j = i + 1; j < kokoko.Length; j++)
			{
				if (kokoko[i].GetComponent<Cube>().ind > kokoko[j].GetComponent<Cube>().ind)
				{
					GameObject temp = kokoko[i];
					kokoko[i] = kokoko[j];
					kokoko[j] = temp;
				}
			}
		}
		Click cl = GameObject.Find("Main Camera").GetComponent<Click>();
		foreach(GameObject mememe in kokoko)
		{

				Cube cubic = mememe.GetComponent<Cube>();
				(script.cubes[cubic.ind] as ArrayList).Remove(mememe);
				if((script.cubes[cubic.ind] as ArrayList).Count < 1){
					script.cubes.RemoveAt(cubic.ind);
					script.cords.RemoveAt(cubic.ind);
					GameObject prt = script.points[cubic.ind] as GameObject;
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
					if(!cl.tags.Contains(max))cl.tags.Add(max);
					cl.tags.Sort();
				}
			(script.cubes [0] as ArrayList).Remove (mememe);
			area.units.Remove(mememe);
			Destroy(mememe);
				
			}
	}

	void menu()
	{
		Application.LoadLevel("Menu");
	}
	
	void tiger()
	{
		ti = true;
		ta = false;
		tr = false;

		tmp_tiger.SetActive(true);
	}
	void truck()
	{
		ti = false;
		ta = false;
		tr = true;

		tmp_truck.SetActive(true);
	}
	void tank()
	{
		ti = false;
		ta = true;
		tr = false;

		tmp_tank.SetActive(true);
	}
}
