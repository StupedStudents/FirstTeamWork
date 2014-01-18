using UnityEngine;
using System.Collections;

public class Escalator : MonoBehaviour {

	public Vector3 direction;
	public bool side = false, fl = true;

	void Start()
	{
		direction = this.transform.TransformDirection(direction);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Particle")
		{
			fl = false;
			return;
		}

		if((col.tag == "Cub" || col.tag == "Current"))
		{
				if(Vector3.Distance(this.transform.position,
				                    (Vector3) GameObject.Find("Terrain").GetComponent<Script>().cords[col.GetComponent<Cube>().ind])
				   < Vector3.Distance(col.transform.position,
				                   (Vector3) GameObject.Find("Terrain").GetComponent<Script>().cords[col.GetComponent<Cube>().ind]))
				{
					if(col.transform.position.x -  
					   ((Vector3) GameObject.Find("Terrain").GetComponent<Script>().cords[col.GetComponent<Cube>().ind]).x  > 10)
					{
						col.GetComponent<Cube>().dir = 1;
					}
					else if(col.transform.position.x -  
				        ((Vector3) GameObject.Find("Terrain").GetComponent<Script>().cords[col.GetComponent<Cube>().ind]).x  < -10)
					{
						col.GetComponent<Cube>().dir = -1;
					}
					else
					{
						col.GetComponent<Cube>().dir = 0;
					}
						
				}
				else
				{
					col.GetComponent<Cube>().dir = 0;
				}

		}
	}

	void OnTriggerStay(Collider col)
	{
		if((col.tag == "Cub" || col.tag == "Current") && col.GetComponent<Cube>().dir != 0 && fl)
		{
			col.transform.constantForce.force = 30f * direction;
			if(side)
			{
				col.transform.constantForce.force *= col.GetComponent<Cube>().dir;
			}

		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Particle")
		{
			fl = true;
			return;
		}
		if((col.tag == "Cub" || col.tag == "Current"))
		{
			col.GetComponent<Cube>().dir = 2;
		}

	}
}
