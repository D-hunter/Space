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

	string Name;
	float Weight;
	float Radius;
	float declX;
	float declZ;
	float declY;
	float Eks;
	float Period;

	public GameObject ship;
	bool isNitro = false;

	public float MAccs = 0.1f;
	public float MaxAccs = 0.7f;

	void Start ()
	{
		heightCoeficient = 444f / Screen.height;
		widthCoeficient = 1040f / Screen.width;
		ship = GameObject.FindGameObjectWithTag("StarDestroyer");

		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25)* widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 100) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 4) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, (Screen.height / 4) * heightCoeficient);
	}
	
	void Update ()
	{
		GetPlanetInfo ();
<<<<<<< HEAD
		planetInfo = new Rect ((Screen.width - (Screen.width / 4) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, (Screen.height / 4) * heightCoeficient);
=======
		planetInfo = new Rect ((Screen.width - 210) * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 200 * heightCoeficient);
>>>>>>> da981424ba923f6b7b51b3c8316ef8e1129275fb
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (exitButton), "Залишити програму")) {
			Application.Quit ();
		}

		if (GUI.Button (new Rect (bustButton), "GO!")) {
			if ((ship.GetComponent<SpaceShipPhysics>().Acceleration + MAccs) < MaxAccs)
				ship.GetComponent<SpaceShipPhysics>().Acceleration += MAccs;
		}

		GUI.TextArea (new Rect (planetInfo), 
		    " Назва планети: " + Name +
		    "\n Вага планети: " + Weight +
			"\n Радіус: " + Radius + 
			"\n Нахил орбіти по осі Х: " + declX + 
			"\n Нахил орбіти по осі Y: " + declY +
			"\n Нахил орбіти по осі Z: " + declZ +
			"\n Ексцентриситет орбіти: " + Eks + 
			"\n Період обертання: " + Period);

		if (GUI.Button (new Rect (flyButton), "Вилетіти")) {
			ship.GetComponent<SpaceShipPhysics>().SdLaunched = true;
			ship.GetComponent<SpaceShipPhysics>().DestPlanet = GameObject.Find(planet.name);//!!!
		}
	}

	void GetPlanetInfo ()
	{
		try
		{
		planet = Camera.main.GetComponent<CameraController>().OrbitTarget;
		data = planet.GetComponent<PlanetMovement>();
		Name = planet.name;
		Weight = data.Wheight;
		Radius = data.Radius;
		declX = data.declX;
		declY = data.declY;
		declZ = data.declZ;
		Eks = data.Eks;
		Period = data.Period;
		}
		catch(System.Exception e)
		{
		}
	}
}
