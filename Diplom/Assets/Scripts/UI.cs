using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	public float heightCoeficient = 0f;
	public float widthCoeficient = 0f;

	public static Rect exitButton;
	public static Rect bustButton;
	

	void Start ()
	{
		heightCoeficient = 444f / Screen.height ;
		widthCoeficient = 1040f / Screen.width;

		exitButton = new Rect(10 * widthCoeficient,10 * heightCoeficient,200 * widthCoeficient,50 * heightCoeficient);
		bustButton = new Rect(Screen.width / 2 * widthCoeficient,10 * heightCoeficient,50 * widthCoeficient,50 * heightCoeficient);
	}
	
	void Update ()
	{
	
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(exitButton),"Залишити програму"))
		{
			Application.Quit();
		}
		if(GUI.Button(new Rect(bustButton),"GO!"))
		{
			Debug.Log("Burning Out!!!");
		}
	}
}
