using UnityEngine;
using System.Collections;

public class Contr : MonoBehaviour {

	public float alpha = 0;
	public float epsilon = 0;
	public float phi = 0;

	void Start()
	{
		GameObject.Find("alp").GetComponent<UILabel>().text = alpha.ToString();
		GameObject.Find("eps").GetComponent<UILabel>().text = epsilon.ToString();
		GameObject.Find("ph").GetComponent<UILabel>().text = phi.ToString();

	}

	void Update()
	{
		alpha = GameObject.Find("alpha").GetComponent<UISlider>().sliderValue * 10;
		epsilon = GameObject.Find("epsilon").GetComponent<UISlider>().sliderValue * 100;
		phi = GameObject.Find("phi").GetComponent<UISlider>().sliderValue * 10;
		GameObject.Find("alp").GetComponent<UILabel>().text = alpha.ToString();
		GameObject.Find("eps").GetComponent<UILabel>().text = epsilon.ToString();
		GameObject.Find("ph").GetComponent<UILabel>().text = phi.ToString();
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
		GameObject.Find("alpha").GetComponent<UISlider>().sliderValue = Script.alpha / 10f;
		GameObject.Find("epsilon").GetComponent<UISlider>().sliderValue = Script.epsilon / 100f;
		GameObject.Find("phi").GetComponent<UISlider>().sliderValue = Script.phi / 10f;
	}

	void apply()
	{
		Script.alpha = alpha;
		Script.epsilon = epsilon;
		Script.phi = phi;
	}

	void retn()
	{
		GameObject.Find("alpha").GetComponent<UISlider>().sliderValue = Script.alpha / 10f;
		GameObject.Find("epsilon").GetComponent<UISlider>().sliderValue = Script.epsilon / 100f;
		GameObject.Find("phi").GetComponent<UISlider>().sliderValue = Script.phi / 10f;	
	}
}
