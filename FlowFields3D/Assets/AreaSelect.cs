using UnityEngine;
using System.Collections;

public class AreaSelect : MonoBehaviour
{
		public GameObject[] units;
		public Vector2 mouseButton1DownPoint;
		public Vector2 mouseButton1UpPoint;
		public Vector3 mouseButton1DownTerrainHitPoint;
		public Vector2 mouseButton2DownPoint;
		public Vector2 mouseButton2UpPoint;
		public Vector3 selectionPointStart;
		public Vector3 selectionPointEnd;
		public bool mouseLeftDrag = false;
		public int terrainLayerMask;
		public int nonTerrainLayerMask;
		public float raycastLength = 1000.0f;
		Texture selectionTexture;
		public int mouseButtonReleaseBlurRange = 1;	
		public Transform terrainPointerForMovement;
		public RaycastHit hit;
		public LayerMask terrainMaskForMovement;

		void Start ()
		{
				units = GameObject.FindGameObjectsWithTag ("Cub");
				selectionTexture = GameObject.FindGameObjectWithTag ("Player").renderer.material.mainTexture;
				terrainLayerMask = 1 << 0;
				nonTerrainLayerMask = ~(1 << 0);
		}
	
		void OnGUI ()
		{
				if (mouseLeftDrag) {	
						int width = (int)(mouseButton1UpPoint.x - mouseButton1DownPoint.x);
						int height = (int)((Screen.height - mouseButton1UpPoint.y) - (Screen.height - mouseButton1DownPoint.y));
						Rect rect = new Rect (mouseButton1DownPoint.x, Screen.height - mouseButton1DownPoint.y, width, height);
						GUI.DrawTexture (rect, selectionTexture, ScaleMode.StretchToFill, true);
				}
		}
	
		void Update ()
		{


				if (Input.GetMouseButtonDown (0)) {  
						Mouse1Down (Input.mousePosition);
				}
				if (Input.GetMouseButtonUp (0)) {
						mouseLeftDrag = false;
				}
				if (Input.GetMouseButton (0)) {
						Mouse1DownDrag (Input.mousePosition);
				}	
				if (Input.GetKey ("escape")) {
						mouseButton2DownPoint = Input.mousePosition;
				}   
		}

		void Mouse1DownDrag (Vector2 screenPosition)
		{

				if (screenPosition != mouseButton1DownPoint) {
						mouseLeftDrag = true;
						mouseButton1UpPoint = screenPosition;	
						RaycastHit hit;
						Ray ray = camera.ScreenPointToRay (screenPosition); 		
						if (Physics.Raycast (ray, out hit, raycastLength, terrainLayerMask)) { 
								selectionPointEnd = hit.point;
								SelectUnitsInArea (selectionPointStart, selectionPointEnd);
						}	
				}
		}
	
		void Mouse1Down (Vector2 screenPosition)
		{
				mouseButton1DownPoint = screenPosition;
				RaycastHit hit;
				Ray ray = camera.ScreenPointToRay (mouseButton1DownPoint);  
				if (Physics.Raycast (ray, out hit, raycastLength, 1 << 0)) { 
						if (hit.collider.tag == "Terrain") {
								mouseButton1DownTerrainHitPoint = hit.point;
								selectionPointStart = hit.point;
						} else {
								GameObject[] arr;
								arr = GameObject.FindGameObjectsWithTag ("Current");
								foreach (GameObject tmp in arr) {
										tmp.tag = "Cub";
								}
						}
				}
		}

		public void SelectUnitsInArea (Vector3 point1, Vector3 point2)
		{
				if (point2.x < point1.x) {
						float x1 = point1.x;
						float x2 = point2.x;
						point1.x = x2;
						point2.x = x1;
				}	
				if (point2.z > point1.z) {
						float z1 = point1.z;
						float z2 = point2.z;
						point1.z = z2;
						point2.z = z1;
				}	
				foreach (GameObject go in units) {
						Vector3 goPos = go.transform.position;
						if (goPos.x > point1.x && goPos.x < point2.x && goPos.z < point1.z && goPos.z > point2.z) {
								go.tag = "Current";
						}
						else
						{
							go.tag = "Cub";
						}
				}
		}	
}
