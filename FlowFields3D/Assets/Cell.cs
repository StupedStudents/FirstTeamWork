using UnityEngine;
using System.Collections;


public class Cell : MonoBehaviour {
	
	public float dist;
	public Cube cubic;
	public Script script;
	public void distansce(int ind, Vector3 pos){
		dist = Mathf.Sqrt (Mathf.Pow (((Vector3)(script.cords[ind])).x - pos.x, 2) + Mathf.Pow (((Vector3)(script.cords[ind])).z - pos.z, 2));
	}
	public Vector3 magnitude(int ind, Vector3 pos){
		Vector3 force = new Vector3 (Mathf.Abs((((Vector3)(script.cords[ind])).x - pos.x)) * ((((Vector3)(script.cords[ind])).x - pos.x) / dist), 0,
		                             Mathf.Abs((((Vector3)(script.cords[ind])).z - pos.z)) * ((((Vector3)(script.cords[ind])).z - pos.z) / dist));
		
		if (Mathf.Abs(force.x) > 10F) {
			force.x = 10F * Mathf.Sign(force.x);
		}
		if (Mathf.Abs(force.z) > 10F) {
			force.z = 10F * Mathf.Sign(force.z);
		}
		return force * 3f;
	}
	void Start () {
		script = GameObject.Find("Terrain").GetComponent<Script>();
	}
	public Vector3 calcInfluence(int ind, Vector3 pos){
		 
		distansce (ind, pos);
		if (dist != 0) {
			return (magnitude (ind,pos));
		} 
		else return(new Vector3 (0, 0, 0));
	}
	public void OnTriggerEnter(Collider col)
	{

	}
	void Update () {

	}
}
