using UnityEngine;
using System.Collections;

public class SpaceShipPhysics : MonoBehaviour {

	public float Acceleration;
	public float StartAngle = 0.0f;
	public float DeltaTime = 0.05f;
	public Vector3 RadiusVector;

	private static float _OrbitHeight = 0.3f;
	//***************************************
	//Physic objects variables***************
	private GameObject StarDestroyer;
	private GameObject Mercury;
	private GameObject Venus;
	private GameObject Earth;
	private GameObject Mars;
	private GameObject Jupiter;
	private GameObject Saturn;
	private GameObject Uranus;
	private GameObject Neptun;
	private GameObject StartPlanet;
	private GameObject DestPlanet;
	//***************************************
	// Use this for initialization
	void Start () {
		//************************************************
		//Planet objects initialization*******************
		StarDestroyer = GameObject.Find ("StarDestroyer");
		Mercury = GameObject.Find ("Mercury");
		Venus = GameObject.Find ("Venus");
		Earth = GameObject.Find ("Earth");
		Mars = GameObject.Find ("Mars");
		Jupiter = GameObject.Find ("Jupiter");
		Saturn = GameObject.Find ("Saturn");
		Uranus = GameObject.Find ("Uran");
		Neptun = GameObject.Find ("Neptun");
		//*************************************************
		StartPlanet = Earth;
		DestPlanet = Jupiter;
		RadiusVector = UnproResizeSum(StartPlanet.transform.localScale, new Vector3(0.3f, 0.3f, 0.3f));
	}
	//******************************
	//Spaceship state controllers***
	private bool SdLaunched = false;
	private bool SdArrived = false;
	private bool SdWayFound = false;
	//******************************

	// Update is called once per frame
	void Update () {
		if (!SdLaunched) {
			OrbitRotation(StartPlanet);
		}
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

	/*bool Direction(Vector3 test, Vector3 axisdir){
	}*/
	//*********************************************************************************

	/*Vector3 GetPower(GameObject powersource, GameObject powerslave){
		//powersource - planet, powerslave - ship

	}*/
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
