using UnityEngine;
using System.Collections;

public class SpaceShipMechanic : MonoBehaviour {
	private static string _SpaceShipName = "StarDestroyer";
	private static string _StartPlanetName = "Earth";
	private static string _DestPlanetName = "Jupiter";
	private GameObject StarDestoyer;
	private GameObject StartPlanet;
	private GameObject DestPlanet;
	public float ORLVal;

	public float ShipReAcceleration;			// Reverse acceleration more - slower
	public Vector3 RadiusVector = new Vector3 ();

	public float StartAngle = 0.0f;
	public float DeltaTime = 0.05f;				// orbit speed

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
				//RadiusVector = new Vector3(6.7f, 6.7f, 6.7f);
			}
		}
		if (ShipStarted && !ShipArrived) {
			if (!RouteFound){
				//calculate route
				float x = RadiusVector.x * Mathf.Sin (StartAngle+DeltaTime) 
					+ StartPlanet.transform.position.x;
				float z = RadiusVector.z * Mathf.Cos (StartAngle+DeltaTime) 
					+ StartPlanet.transform.position.z;

				Vector3 nextStep = new Vector3(x, 0, z).normalized;

				Vector3 SpeedVector = new Vector3(nextStep.x - StarDestoyer.transform.position.x,
				                                  nextStep.y - StarDestoyer.transform.position.y,
				                                  nextStep.z - StarDestoyer.transform.position.z).normalized;

				Vector3 CourseToPlanet = new Vector3(
					DestPlanet.transform.position.x - StarDestoyer.transform.position.x,
					DestPlanet.transform.position.y - StarDestoyer.transform.position.y,
					DestPlanet.transform.position.z - StarDestoyer.transform.position.z).normalized;

					float OrbitLandValue = Vector3.Dot(CourseToPlanet, SpeedVector);
				ORLVal = OrbitLandValue;
				OrbitRotation(StartPlanet);
				if (OrbitLandValue < -0.96){
					RouteFound = true;
					RadiusVector = new Vector3(6.3f, 6.3f, 6.3f);
				}
			}
			else {
				//move to the planet && check arrived conditionq
					Vector3 MoveDirection = new Vector3(
						DestPlanet.transform.position.x - StarDestoyer.transform.position.x,
						DestPlanet.transform.position.y - StarDestoyer.transform.position.y,
						DestPlanet.transform.position.z - StarDestoyer.transform.position.z).normalized;

					Vector3 NewPosition = new Vector3(
					StarDestoyer.transform.position.x + (MoveDirection.x / ShipReAcceleration)*Time.deltaTime,
					StarDestoyer.transform.position.y + (MoveDirection.y / ShipReAcceleration)*Time.deltaTime,
					StarDestoyer.transform.position.z + (MoveDirection.z / ShipReAcceleration)*Time.deltaTime);

					StarDestoyer.transform.position = NewPosition;

					if (Vector3.Distance(StarDestoyer.transform.position, DestPlanet.transform.position) < 6.3f)
						ShipArrived = true;
			}
		}
		if (ShipArrived) {
			//rotation on orbit
			OrbitRotation (DestPlanet);
		}
	}
}
