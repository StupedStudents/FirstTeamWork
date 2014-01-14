using UnityEngine;
using System.Collections;

public class Contr : MonoBehaviour {

	public float alpha = 0;
	public float epsilon = 0;
	public float phi = 0;

	void Start()
	{
		GameObject.Find("alpha").GetComponent<UISlider>().sliderValue = 0.15f;
		GameObject.Find("epsilon").GetComponent<UISlider>().sliderValue = 0.3f;
		GameObject.Find("phi").GetComponent<UISlider>().sliderValue = 0.2f;

	}

	void Update()
	{
		alpha = GameObject.Find("alpha").GetComponent<UISlider>().sliderValue * 10;
		epsilon = GameObject.Find("epsilon").GetComponent<UISlider>().sliderValue * 100;
		phi = GameObject.Find("phi").GetComponent<UISlider>().sliderValue * 10;
	}
	void newGame()
	{
		Application.LoadLevel("Tmp");
	}

	void exit()
	{
		Application.Quit();
	}

	void options()
	{

	}

	void apply()
	{
		
	}

	void retn()
	{
		
	}
}
