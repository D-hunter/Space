using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	GameObject planet;
	PlanetMovement data;
	float heightCoeficient = 0f;
	float widthCoeficient = 0f;
	Rect exitButton;
	Rect bustButton;
	Rect planetInfo;
	Rect flyButton;
	float Weight;
	float Radius;
	float declX;
	float declZ;
	float declY;
	float Eks;
	float Period;

	void Start ()
	{
		heightCoeficient = 444f / Screen.height;
		widthCoeficient = 1040f / Screen.width;

		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25)* widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect ((Screen.width / 2 - 50) * widthCoeficient, (Screen.height - 100) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - 210) * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 200 * heightCoeficient);
	}
	
	void Update ()
	{
		GetPlanetInfo ();
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (exitButton), "Залишити програму")) {
			Application.Quit ();
		}
		if (GUI.Button (new Rect (bustButton), "GO!")) {
			Debug.Log ("Burning Out!!!");
		}

		GUI.TextArea (new Rect (planetInfo), "Вага планети: " + Weight +
			"\n Радіус: " + Radius + 
			"\n Нахил орбіти по осі Х: " + declX + 
			"\n Нахил орбіти по осі Y: " + declY +
			"\n Нахил орбіти по осі Z: " + declZ +
			"\n Ексцентриситет орбіти: " + Eks + 
			"\n Період обертання: " + Period);

		if (GUI.Button (new Rect (flyButton), "Вилетіти")) {
		}
	}

	void GetPlanetInfo ()
	{
		planet = Camera.main.GetComponent<CameraController> ().OrbitTarget;
		data = planet.GetComponent<PlanetMovement> ();
		Weight = data.Wheight;
		Radius = data.Radius;
		declX = data.declX;
		declY = data.declY;
		declZ = data.declZ;
		Eks = data.Eks;
		Period = data.Period;

	}
}
