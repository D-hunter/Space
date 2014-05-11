using UnityEngine;
using System.Collections;

public class SpaceShipPhysics : MonoBehaviour {

	public float Acceleration;
	public float Attenuation;
	public float StartAngle = 0.0f;
	public float DeltaTime = 0.05f;
	public Vector3 RadiusVector;

	private static int count_of_planets = 9;
	private static int IndexOfPlanet = 2;
	private static float _OrbitHeight = 0.3f;
	private Vector3 CenterForceDir;
	private Vector3 MoveGravitySun;
	private float CenterForce;
	private static float ForceScale = 1053f;
	private static Vector3 GlobalForceScale = new Vector3 (0.1f, 0.1f, 0.1f);
	private float maxAcceleration = 0.3f;
	private float minAcceleration = 0.001f;
	private Vector3 ShipAccelerationDir;
	private Vector3 DestPlanetLastPos;

	//***************************************
	//Physic objects variables***************

	private GameObject[] Planets = new GameObject[count_of_planets];
	private float[] RPlanets = {
		6959.90f,   
		24.39f,	//10^5m
		60.52f,
		63.78f,
		34.88f,
		713.0f,
		601.0f,
		265.0f,
		247.5f
	};

	private float[] MPlanets = {
		1989000f,
		0.32868f,	//10^24kg
		4.81068f,
		5.976f,
		0.63345f,
		1876.64328f,
		561.80376f,
		86.0544f,
		101.592f,
	};

	private GameObject StarDestroyer;
	private GameObject StartPlanet;
	private GameObject DestPlanet;

	private float G = 0.67f;
	//***************************************
	// Use this for initialization
	void Start () {
		//************************************************
		//Planet objects initialization*******************
		StarDestroyer = GameObject.Find ("StarDestroyer");
		Planets[0] = GameObject.Find ("Sun");
		Planets[1] = GameObject.Find ("Mercury");
		Planets[2] = GameObject.Find ("Venus");
		Planets[3] = GameObject.Find ("Earth");
		Planets[4] = GameObject.Find ("Mars");
		Planets[5] = GameObject.Find ("Jupiter");
		Planets[6] = GameObject.Find ("Saturn");
		Planets[7] = GameObject.Find ("Uran");
		Planets[8] = GameObject.Find ("Neptun");
		//*************************************************
		StartPlanet = Planets[3];
		DestPlanet = Planets[IndexOfPlanet];
		RadiusVector = UnproResizeSum(StartPlanet.transform.localScale, new Vector3(0.3f, 0.3f, 0.3f));

		Vector3 FirstPos = StartPlanet.transform.position;
		float z = StartPlanet.transform.position.z * Mathf.Cos (StartAngle + 0.1f);
		Vector3 SecondPos = new Vector3(1, 0, z);
		CenterForce = VectDif (SecondPos, FirstPos).magnitude;
		DestPlanetLastPos = DestPlanet.transform.position;
	}
	//******************************
	//Spaceship state controllers***
	private bool SdLaunched = false;
	private bool SdArrived = false;
	private bool SdOnRbit = false;
	//******************************

	// Update is called once per frame
	void Update () {

		MoveGravitySun = GetPower (Planets[0], StarDestroyer, 0).normalized;
		CenterForceDir = MRotVect (MoveGravitySun, MRotZ (Mathf.PI/2.0f)).normalized;

		if (!SdLaunched) {
			OrbitRotation (StartPlanet);
			if (Input.GetKey(KeyCode.J))
			    SdLaunched = true;
		} else {
			if (!SdArrived){
				Vector3 MoveToGravity = new Vector3(); 
				for (int i = 0; i < 9; i++)
					MoveToGravity += GetPower (Planets[i], StarDestroyer, i);
				StarDestroyer.transform.position += Vector3.Scale(MoveToGravity, GlobalForceScale);
				//Сила ускорения вокругсолнечного вращения перпендикулярна силе притяжения Солнца
				StarDestroyer.transform.position += Vector3.Scale(new Vector3(CenterForceDir.x * CenterForce, 0,
			                                                	CenterForceDir.z * CenterForce), GlobalForceScale);
				if(Input.GetKey(KeyCode.A)){
					if ((Acceleration += minAcceleration) > maxAcceleration)
						Acceleration -= minAcceleration;
					//ShipAccelerationDir = VectDif(DestPlanet.transform.position, StarDestroyer.transform.position).normalized;
					MoveGravitySun = GetPower (Planets[0], StarDestroyer, 0).normalized;
					if (UnproResizeSum(VectDif(DestPlanet.transform.position, StarDestroyer.transform.position), StarDestroyer.transform.position).magnitude >
					    StarDestroyer.transform.position.magnitude)
						ShipAccelerationDir = VectDif(new Vector3(), MoveGravitySun);
					else
						ShipAccelerationDir = MoveGravitySun;
	
				}
				Vector3 SpaceAccelerationToCoord = LongPower(ShipAccelerationDir, ref Acceleration, Attenuation);
				StarDestroyer.transform.position += Vector3.Scale(SpaceAccelerationToCoord, GlobalForceScale);
	
				if (VectDif(StarDestroyer.transform.position, DestPlanet.transform.position).magnitude < DestPlanet.transform.localScale.magnitude + 5.0f){
					SdArrived = true;
					RadiusVector = UnproResizeSum(DestPlanet.transform.localScale, new Vector3(0.05f, 0.05f, 0.05f));
					DeltaTime /= DestPlanet.transform.localScale.magnitude/5.0f;
				}	
			}
		}
		if (SdArrived) {
			if (!SdOnRbit){
				Vector3 OrbitDistance = VectDif(DestPlanet.transform.position, StarDestroyer.transform.position);
				float DestPlanetSpeed = VectDif(DestPlanet.transform.position, DestPlanetLastPos).magnitude * 1.5f;
				Vector3 ToOrbitLandPoint = MRotVect(OrbitDistance, MRotZ(Mathf.PI/4.0f)).normalized;

				StarDestroyer.transform.position += new Vector3 (ToOrbitLandPoint.x * DestPlanetSpeed*2.0f,
				                                                 ToOrbitLandPoint.y * DestPlanetSpeed*2.0f,
				                                                 ToOrbitLandPoint.z * DestPlanetSpeed*2.0f);

				StartAngle = Vector3.Angle(VectDif(DestPlanet.transform.position, StarDestroyer.transform.position), new Vector3(0, 0, 1)) * Mathf.Deg2Rad - Mathf.PI/2.0f;

				if (OrbitDistance.magnitude <= RadiusVector.magnitude*0.5f)
					SdOnRbit = true;
			}
			if (SdOnRbit)
				OrbitRotation(DestPlanet);
		}
		DestPlanetLastPos = DestPlanet.transform.position;
	}
	//*********************************************************************************
	//Vector3 additional ariphmetic****************************************************
	Vector3 VectDif(Vector3 begin, Vector3 End){
		return new Vector3 (begin.x - End.x, begin.y - End.y, begin.z - End.z);
	}

	Vector3 UnproResizeSum(Vector3 left, Vector3 right){
		return new Vector3 (left.x + right.x, left.y + right.y, left.z + right.z);
	}

	Vector3 VectDifNormalize(Vector3 begin, Vector3 End){
		return new Vector3 (begin.x - End.x, begin.y - End.y, begin.z - End.z).normalized;
	}

	Vector3 NumberToVector3(float number, Vector3 distance){
		Vector3 direction = distance.normalized;
		return new Vector3 (direction.x * number,
		                    direction.y * number,
		                    direction.z * number);
	}

	/*bool Direction(Vector3 test, Vector3 axisdir){
	}*/
	//*********************************************************************************

	Vector3 GetPower(GameObject powersource, GameObject powerslave, int index){
		//powersource - planet, powerslave - ship
		float power = G * MPlanets[index] / (VectDif (powersource.transform.position,
		                                      powerslave.transform.position).sqrMagnitude*ForceScale);

		return NumberToVector3 (power, VectDif (powersource.transform.position,
		                                       powerslave.transform.position));
	}

	Vector3 LongPower (Vector3 direction, ref float value, float attenuation){
		if ((value -= attenuation) < 0)
			value = 0;
		return new Vector3 (direction.x * value,
		                   direction.y * value,
		                   direction.z * value);
	}

	public float[,] MRotZ(float angle){
		return new float[,] {
			{Mathf.Cos (angle), 0, Mathf.Sin(angle)},
			{0, 1, 0},
			{-Mathf.Sin(angle), 0, Mathf.Cos(angle),}};
	}

	public Vector3 MRotVect(Vector3 pos, float[,] mrotax)
	{
		float x = 0.0f, y = 0.0f, z = 0.0f;
		x = pos.x * mrotax [0, 0] + pos.y * mrotax [0, 1] + pos.z * mrotax [0, 2];
		y = pos.x * mrotax [1, 0] + pos.y * mrotax [1, 1] + pos.z * mrotax [1, 2];
		z = pos.x * mrotax [2, 0] + pos.y * mrotax [2, 1] + pos.z * mrotax [2, 2];
		return new Vector3 (x, y, z);
	}
	//Vector3 ReForcement(){
	//}

	//*********************************************************************************
	//Standart movement function*******************************************************
	public void OrbitRotation(GameObject planet)
	{
		Vector3 PlanetRotOffset = planet.transform.position;
		float x = RadiusVector.x * Mathf.Sin (StartAngle) + PlanetRotOffset.x;
		float z = RadiusVector.z * Mathf.Cos (StartAngle) + PlanetRotOffset.z;
		
		StarDestroyer.transform.position = new Vector3(x, 0, z);

		if ((StartAngle -= DeltaTime) < Mathf.PI * -2)
			StartAngle = 0.0f;
	}
	//*********************************************************************************

	//*********************************************************************************
	// Physics additional functions****************************************************

	//*********************************************************************************
}
