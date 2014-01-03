using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public float camSpeed = 100; //скорость перемещения камеры в плоскости
	public float zoomSpeed = 1000; //скорость зума колесом мыши
	public float mouseSensitivity = 100; //чувствительность мыши при вращении камеры  
	
	private Transform _myTransform; //здесь кэширую свойство transform камеры
	// Use this for initialization
	void Start () {
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//преобразуем скорость по фреймам в скорость по времени
		float speedMod = camSpeed * Time.deltaTime;
		float zoomMod = zoomSpeed * Time.deltaTime;
		float sensMod = mouseSensitivity * Time.deltaTime;  
		
		//движение камеры влево-вправо
		_myTransform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speedMod, Space.Self);  
		
		//уже поинтересней, движение вперед-назад
		_myTransform.Translate(Vector3.Scale(_myTransform.TransformDirection(Vector3.forward), new Vector3(1,0,1)).normalized *
		                       Input.GetAxis("Vertical") * speedMod, Space.World);  
		
		//приближение-удаление
		_myTransform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * zoomMod, Space.Self);  
		
		//поворот камеры
		if (Input.GetMouseButton(1))
		{
			_myTransform.Rotate(-Input.GetAxis("Mouse Y") * sensMod, 0, 0, Space.Self);
			_myTransform.Rotate(0, Input.GetAxis("Mouse X") * sensMod, 0, Space.World);
		}
	}
}
