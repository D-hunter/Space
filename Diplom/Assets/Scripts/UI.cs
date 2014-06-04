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

	Vector3 scale;

	public GameObject ship;
	bool isNitro = false;
	bool isFlyEnded = false;
	public float MAccs = 0.1f;
	public float MaxAccs = 99f;
	public GUIStyle style = new GUIStyle ();

	float originalHeight = 444f;
	float originalWidth = 1040f;

	void Start ()
	{
		heightCoeficient = Screen.height / originalHeight;
		widthCoeficient = Screen.width / originalWidth;
		scale = new Vector3(widthCoeficient,heightCoeficient,1);
		ship = GameObject.FindGameObjectWithTag ("StarDestroyer");
		shipPhys = ship.GetComponent<SpaceShipPhysics>();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;

		exitButton = new Rect (10 , 10 , originalWidth / 7, originalHeight / 10 );
		bustButton = new Rect ((originalWidth / 2 - originalWidth / 20) , 10 , originalWidth / 10 , originalHeight / 10  );
		flyButton = new Rect (10 , (originalHeight - 50) , originalWidth / 7 , originalHeight / 10 );
		planetInfo = new Rect ((originalWidth - (originalWidth / 4) - 10) , 10 , (originalWidth / 4) , 200);
		sheepInfo = new Rect ((originalWidth - (originalWidth / 4) - 10) , planetInfo.height + 50, (originalWidth / 4) , 100 );
		endFlyMessage = new Rect ((originalWidth / 2 - originalWidth / 10) , (originalHeight / 1.1f - 50) , originalWidth / 5 , 100 );
		InitializePlanetsButtonsRects();
	}

	void FixedUpdate ()
	{
		heightCoeficient = Screen.height / originalHeight;
		widthCoeficient = Screen.width / originalWidth;
		scale = new Vector3(widthCoeficient,heightCoeficient,1);
		
		exitButton = new Rect (10 , 10 ,  originalWidth / 7 , originalHeight / 10 );
		bustButton = new Rect ((originalWidth / 2 - originalWidth / 20) , 10 ,originalWidth / 10 , originalHeight / 10  );
		flyButton = new Rect (10 , (originalHeight - 50) , originalWidth / 7 , originalHeight / 10 );
		planetInfo = new Rect ((originalWidth - (originalWidth / 4) - 10) , 10 , (originalWidth / 4) , 200 );
		sheepInfo = new Rect ((originalWidth - (originalWidth / 4) - 10) , planetInfo.height + 50, (originalWidth / 4) , 100 );
		endFlyMessage = new Rect ((originalWidth / 2 - originalWidth / 10) , (originalHeight / 1.1f - 50) , originalWidth / 5 , 100 );
		InitializePlanetsButtonsRects();
		GetPlanetInfo ();
	}

	void OnGUI ()
	{
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

		if (GUI.Button (new Rect (exitButton), "Залишити програму")) {
			Application.Quit ();
		}

		if (GUI.Button (new Rect (bustButton), "Прискорення!")) {
			if ((ship.GetComponent<SpaceShipPhysics> ().Acceleration + MAccs) < MaxAccs)
				ship.GetComponent<SpaceShipPhysics> ().Acceleration += MAccs;
		}

		GUI.TextArea (new Rect (planetInfo), 
		    " Назва планети: " + Name +
			"\n Маса планети (ЗетаТони): " + PlanetWeight +
			"\n Діаметр: " + Diameter + 
			"\n Період обертання навколо сонця: " + RotationPeriod + 
		    "\n Період обертання навколо совєї осі: " + SelfRotPeriod +
			"\n Атмосфера: " + Atmosphere +
			"\n Температура: " + Temperature +
			"\n Ексцентриситет орбіти: " + Eks + 
			"\n Кількість супутників: " + SateliteCount + 
		    "\n Головний супутник: " + MainSatelite);

		GUI.TextArea (new Rect(sheepInfo),
		              " Швидкість корабля (км/сек): " + ship.GetComponent<SpaceShipPhysics>().UICurrentSpeed + 
		              "\n Пройдений шлях (а.о.): " + ship.GetComponent<SpaceShipPhysics>().UIPathLength + 
		              "\n " + ship.GetComponent<SpaceShipPhysics>().UIFlyTimeCon);

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
		PlanetsButtons[0] = new Rect(exitButton.x,exitButton.y + 30, exitButton.width, originalHeight / 20 );
		
		for (int i = 1; i < PlanetsButtons.Length; i++) 
		{
			PlanetsButtons[i] = new Rect(PlanetsButtons[i - 1].x,PlanetsButtons[i - 1].y + 30, PlanetsButtons[i - 1].width, originalHeight / 20  );
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
