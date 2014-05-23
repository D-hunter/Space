using UnityEditor;
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
	Rect endFlyMessage;

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
	bool isFlyEnded = false;

	public float MAccs = 0.1f;
	public float MaxAccs = 0.7f;

	public GUIStyle style = new GUIStyle();
	void Awake()
	{

	}

	void Start ()
	{
		heightCoeficient = 444f / Screen.height;
		widthCoeficient = 1040f / Screen.width;
		ship = GameObject.FindGameObjectWithTag("StarDestroyer");
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;


		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25)* widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient,(Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
	}
	
	void Update ()
	{
		GetPlanetInfo ();
	}

	void FixedUpdate()
	{
		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25)* widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient,(Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
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

		if (ship.GetComponent<SpaceShipPhysics>().SdArrived) 
		{
			GUI.TextField(endFlyMessage,"Корабель прилетів до планети!",style);
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

	void GetPlanetsForFly()
	{
		GameObject[] planets;
		planets = ship.GetComponent<SpaceShipPhysics>().Planets;
	
	}
}
