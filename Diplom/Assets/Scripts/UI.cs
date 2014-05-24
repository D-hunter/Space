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
	Rect sheepInfo;
	Rect flyButton;
	Rect endFlyMessage;

	string Name;
	float RotationPeriod;
	float Diameter;
	float SelfRotPeriod;
	string Temperature;
	string Atmosphere;
	float Eks;
	float SateliteCount;
	string MainSatelite;
	float PlanetWeight;

	public GameObject ship;
	bool isNitro = false;
	bool isFlyEnded = false;
	public float MAccs = 0.1f;
	public float MaxAccs = 0.7f;
	public GUIStyle style = new GUIStyle ();

	void Awake ()
	{

	}

	void Start ()
	{
		heightCoeficient = 444f / Screen.height;
		widthCoeficient = 1040f / Screen.width;
		ship = GameObject.FindGameObjectWithTag ("StarDestroyer");
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;


		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25) * widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		sheepInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, planetInfo.height + (50 * heightCoeficient), (Screen.width / 4) * widthCoeficient, 100 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient, (Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
	}
	
	void Update ()
	{
		GetPlanetInfo ();
	}

	void FixedUpdate ()
	{
		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25) * widthCoeficient, 10 * heightCoeficient, 50 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		sheepInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, planetInfo.height + (50 * heightCoeficient), (Screen.width / 4) * widthCoeficient, 100 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient, (Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (exitButton), "Залишити програму")) {
			Application.Quit ();
		}

		if (GUI.Button (new Rect (bustButton), "GO!")) {
			if ((ship.GetComponent<SpaceShipPhysics> ().Acceleration + MAccs) < MaxAccs)
				ship.GetComponent<SpaceShipPhysics> ().Acceleration += MAccs;
		}

		GUI.TextArea (new Rect (planetInfo), 
		    " Назва планети: " + Name +
			"\n Вага планети: " + PlanetWeight +
			"\n Діаметр: " + Diameter + 
			"\n Період обертання навколо сонця: " + RotationPeriod + 
		    "\n Період обертання навколо совєї осі: " + SelfRotPeriod +
			"\n Атмосфера: " + Atmosphere +
			"\n Температура: " + Temperature +
			"\n Ексцентриситет орбіти: " + Eks + 
			"\n Кількість супутників: " + SateliteCount + 
		    "\n Головний супутник: " + MainSatelite);

		GUI.TextArea (new Rect(sheepInfo),
		              " Швидкість корабля (а.о./день): " + ship.GetComponent<SpaceShipPhysics>().UICurrentSpeed + 
		              "\n Пройдений шлях (а.о.): " + ship.GetComponent<SpaceShipPhysics>().UIPathLength + 
		              "\n Час польоту (дні): " + ship.GetComponent<SpaceShipPhysics>().UIFlyTimeCon);

		if (GUI.Button (new Rect (flyButton), "Вилетіти")) {
			ship.GetComponent<SpaceShipPhysics> ().SdLaunched = true;
			ship.GetComponent<SpaceShipPhysics> ().DestPlanet = GameObject.Find (planet.name);//!!!
			ship.GetComponent<SpaceShipPhysics> ().Acceleration += 0.3f; // start acceleration
		}

		if (ship.GetComponent<SpaceShipPhysics> ().SdArrived) {
			GUI.TextField (endFlyMessage, "Корабель прилетів до планети!", style);
		}

	}

	void GetPlanetInfo ()
	{
		try {
			planet = Camera.main.GetComponent<CameraController> ().OrbitTarget;
			data = planet.GetComponent<PlanetMovement> ();
			Name = planet.name;
			RotationPeriod = data.UISunRotationPeriod;
			Diameter = data.UIPlanetDiametr;
			PlanetWeight = data.UIPlanetWeight;
			SelfRotPeriod = data.UISelfRotationPeriod;
			Atmosphere = data.UIAtmosphere;
			Temperature = data.UITemperature;
			Eks = data.Eks;
			SateliteCount = data.UICountSputnik;
			MainSatelite = data.UIMainSputnik;
		} catch (System.Exception e) {
		}
	}

	void GetPlanetsForFly ()
	{
		GameObject[] planets;
		planets = ship.GetComponent<SpaceShipPhysics> ().Planets;
	
	}
}
