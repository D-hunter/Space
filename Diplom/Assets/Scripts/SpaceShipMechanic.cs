using UnityEngine;
using System.Collections;

public class SpaceShipMechanic : MonoBehaviour {
	private static string _SpaceShipName = "StarDestroyer";
	private static string _StartPlanetName = "Earth";
	private static string _DestPlanetName = "Jupiter";
	private GameObject StarDestoyer;
	private GameObject StartPlanet;
	private GameObject DestPlanet;

	public float ShipAcceleration = 0.01f;		// AO per year
	public Vector3 RadiusVector = new Vector3 ();

	public float StartAngle = 0.0f;
	public float DeltaTime = 0.05f;

	private bool ShipArrived = false;
	private bool ShipStarted = false;
	private bool RouteFound = false;

	public void OrbitRotation(GameObject planet)
	{
		Vector3 PlanetRotOffset = planet.transform.position;
		float x = RadiusVector.x * Mathf.Sin (StartAngle) + PlanetRotOffset.x;
		float z = RadiusVector.z * Mathf.Cos (StartAngle) + PlanetRotOffset.z;
		
		StarDestoyer.transform.position = new Vector3(x, 0, z);
		
		if ((StartAngle -= DeltaTime) < Mathf.PI * -2)
			StartAngle = 0.0f;
	}

	// Use this for initialization
	void Start () {
		StarDestoyer = GameObject.Find (_SpaceShipName);
		StartPlanet = GameObject.Find (_StartPlanetName);
		DestPlanet = GameObject.Find (_DestPlanetName);
		RadiusVector = new Vector3 (0.7f, 0.7f, 0.7f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!ShipStarted) {
			//rotation on orbit
			OrbitRotation(StartPlanet);
			if (Input.GetKey(KeyCode.S)){
				ShipStarted = true;
				RadiusVector = new Vector3(6.7f, 6.7f, 6.7f);
			}
		}
		if (ShipStarted && !ShipArrived) {
			if (!RouteFound){
				//calculate route
				RouteFound = true;
			}
			else {
				//move to the planet && check arrived condition
				//ShipArrived = true;
			}
		}
		if (ShipArrived) {
			//rotation on orbit
			OrbitRotation (DestPlanet);
		}
	}
}
