using UnityEditor;
using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	GameObject planet;
	PlanetMovement data;
	public SpaceShipPhysics shipPhys;
	float heightCoeficient = 0f;
	float widthCoeficient = 0f;
	Rect exitButton;
	Rect bustButton;
	Rect planetInfo;
	Rect sheepInfo;
	Rect flyButton;
	Rect endFlyMessage;

	Rect[] PlanetsButtons = new Rect[9];

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

	void Start ()
	{
		heightCoeficient = 444f / Screen.height;
		widthCoeficient = 1040f / Screen.width;
		ship = GameObject.FindGameObjectWithTag ("StarDestroyer");
		shipPhys = ship.GetComponent<SpaceShipPhysics>();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;


		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25) * widthCoeficient, 10 * heightCoeficient, 100 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		sheepInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, planetInfo.height + (50 * heightCoeficient), (Screen.width / 4) * widthCoeficient, 100 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient, (Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
		InitializePlanetsButtonsRects();
	}
	
	void FixedUpdate ()
	{
		exitButton = new Rect (10 * widthCoeficient, 10 * heightCoeficient, 200 * widthCoeficient, 50 * heightCoeficient);
		bustButton = new Rect ((Screen.width / 2 - 25) * widthCoeficient, 10 * heightCoeficient, 100 * widthCoeficient, 50 * heightCoeficient);
		flyButton = new Rect (10 * widthCoeficient, (Screen.height - 50) * heightCoeficient, 100 * widthCoeficient, 100 * heightCoeficient);
		planetInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, 10 * heightCoeficient, (Screen.width / 4) * widthCoeficient, 200 * heightCoeficient);
		sheepInfo = new Rect ((Screen.width - (Screen.width / 3.7f) + 10) * widthCoeficient, planetInfo.height + (50 * heightCoeficient), (Screen.width / 4) * widthCoeficient, 100 * heightCoeficient);
		endFlyMessage = new Rect ((Screen.width / 2 - 100) * widthCoeficient, (Screen.height / 1.07f - 50) * heightCoeficient, 200 * widthCoeficient, 100 * heightCoeficient);
		InitializePlanetsButtonsRects();
		GetPlanetInfo ();
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (exitButton), "Залишити програму")) {
			Application.Quit ();
		}

		if (GUI.Button (new Rect (bustButton), "Прискорення!")) {
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
			shipPhys.SdLaunched = true;
			shipPhys.DestPlanet = GameObject.Find (planet.name);//!!!
			shipPhys.Acceleration += 0.3f; // start acceleration
		}

		if (ship.GetComponent<SpaceShipPhysics> ().SdArrived) {
			GUI.TextField (endFlyMessage, "Корабель прилетів до планети!", style);
		}

		PlanetButtonsController();
	}

	void GetPlanetInfo ()
	{
		try {
			planet = shipPhys.DestPlanet;
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

	void InitializePlanetsButtonsRects()
	{
		PlanetsButtons[0] = new Rect(exitButton.x,exitButton.y + (50 * heightCoeficient), exitButton.width,30 * heightCoeficient);
		
		for (int i = 1; i < PlanetsButtons.Length; i++) 
		{
			PlanetsButtons[i] = new Rect(PlanetsButtons[i - 1].x,PlanetsButtons[i - 1].y + (40 * heightCoeficient), PlanetsButtons[i - 1].width, 30 * heightCoeficient);
		}
	}

	void PlanetButtonsController()
	{
		if(GUI.Button(new Rect(PlanetsButtons[1]),"Меркурій"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[1];
			SpaceShipPhysics.IndexOfPlanet = 1;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[2]),"Венера"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[2];
			SpaceShipPhysics.IndexOfPlanet = 2;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[3]),"Земля"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[3];
			SpaceShipPhysics.IndexOfPlanet = 3;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[4]),"Марс"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[4];
			SpaceShipPhysics.IndexOfPlanet = 4;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[5]),"Юпітер"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[5];
			SpaceShipPhysics.IndexOfPlanet = 5;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[6]),"Сатурн"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[6];
			SpaceShipPhysics.IndexOfPlanet = 6;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[7]),"Уран"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[7];
			SpaceShipPhysics.IndexOfPlanet = 7;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}
		if(GUI.Button(new Rect(PlanetsButtons[8]),"Нептун"))
		{
			shipPhys.DestPlanet = shipPhys.Planets[8];
			SpaceShipPhysics.IndexOfPlanet = 8;
			Camera.main.GetComponent<CameraController>().OrbitTarget = shipPhys.DestPlanet;
			Camera.main.GetComponent<CameraController>().orbitTarget = shipPhys.DestPlanet.transform;
		}

	}
}
