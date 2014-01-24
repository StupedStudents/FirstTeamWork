using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public float camSpeed = 100; 
	public float zoomSpeed = 30000;
	public float mouseSensitivity = 100;   
	
	private Transform _myTransform; 
	void Start () {
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		float speedMod = camSpeed * Time.deltaTime;
		float zoomMod = zoomSpeed * Time.deltaTime;
		float sensMod = mouseSensitivity * Time.deltaTime;  
		
		_myTransform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speedMod, Space.Self);  
		
		_myTransform.Translate(Vector3.Scale(_myTransform.TransformDirection(Vector3.forward), new Vector3(1,0,1)).normalized *
		                       Input.GetAxis("Vertical") * speedMod, Space.World);  
		
		_myTransform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomMod, Space.Self);  
		
		if (Input.GetMouseButton(1))
		{
			_myTransform.Rotate(-Input.GetAxis("Mouse Y") * sensMod, 0, 0, Space.Self);
			_myTransform.Rotate(0, Input.GetAxis("Mouse X") * sensMod, 0, Space.World);
		}
	}
}
