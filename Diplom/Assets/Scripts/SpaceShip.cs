using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {
	GameObject Planet;            //Ship in orbit of planet
	Vector3 RotCenterOffset;
	Vector3 RadiusVector = new Vector3(0.7f, 0.7f, 0.7f);
	float StartAngle = 0.0f;
	float DeltaTime = 0.1f;

	// Use this for initialization
	void Start () {
		Planet = GameObject.Find ("Earth");	
		this.transform.position = Planet.transform.position + RadiusVector; 
	}
	
	// Update is called once per frame
	void Update () {	
		RotCenterOffset = Planet.transform.position;
		float x = RadiusVector.x * Mathf.Sin (StartAngle) + RotCenterOffset.x;
		float z = RadiusVector.z * Mathf.Cos (StartAngle) + RotCenterOffset.z;

		this.transform.position = new Vector3 (x, 0.0f, z);

		if ((StartAngle -= DeltaTime) < Mathf.PI * -2)
			StartAngle = 0.0f;
	}
}
