using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour {
	public const int m = 100, n = 100;
	public GameObject[,] table = new GameObject[m,n];
	GameObject buf;
	void Start () {
		buf = GameObject.FindGameObjectWithTag("Cell");
		for (int i = 0; i < m; i++) {
			for (int j = 0; j < n; j++) {
				table[i,j] = Instantiate(buf,new Vector3(i - m/2,1,j - n/2),new Quaternion()) as GameObject;
			}
		}
	}
	void Update () {
	
	}
}
