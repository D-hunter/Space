﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform orbitTarget;
	public GameObject OrbitTarget;
	public float distance;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	private float x = 0.0f;
	private float y = 0.0f;
	private GameObject StarDestroyer;
	private GameObject DestPlanet;

	void Start ()
	{
		transform.position = new Vector3 (0, 101, 0);
		distance = transform.position.y;
		StartOrbit ();
		StarDestroyer = GameObject.Find ("StarDestroyer");
	}
	
	void Update ()
	{
		Zoom ();
		RunOrbit ();
		DefaultPosition ();
		SelectTarget();
		if (Input.GetKey (KeyCode.C)) {
			CameraOnShip ();
		}
	}

	void CameraOnShip()
	{
		DestPlanet = StarDestroyer.GetComponent<SpaceShipPhysics> ().DestPlanet;
		Vector3 CamMoveDir = VectDif (DestPlanet.transform.position, StarDestroyer.transform.position).normalized;
		CamMoveDir = VectDif (new Vector3 (), CamMoveDir);
		CamMoveDir *= 10;
		transform.position = StarDestroyer.transform.position + CamMoveDir;
		transform.LookAt (StarDestroyer.transform.position);
	}

	void Zoom ()
	{
		try
		{
		Vector3 movementVector = (orbitTarget.position - transform.position).normalized;
		if ((Input.GetAxis ("Mouse ScrollWheel") < 0) && distance < 600) {
			transform.position -= movementVector * 10;
			distance = transform.position.y;
		} else if ((Input.GetAxis ("Mouse ScrollWheel") > 0) && distance > 15) {
			transform.position += movementVector * 10;
			distance = transform.position.y;
		}
		}
		catch (System.Exception e)
		{
		}
	}
	
	static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360) {
			angle += 360;
		}
		if (angle > 360) {
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}

	void StartOrbit ()
	{
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		if (rigidbody) 
			rigidbody.freezeRotation = true;

	}

	void RunOrbit ()
	{
		if (orbitTarget != null)
			transform.LookAt(orbitTarget);
		if (orbitTarget && Input.GetKey (KeyCode.Mouse1)) 
		{
			x += Input.GetAxis ("Mouse X") * xSpeed * Time.deltaTime;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * Time.deltaTime;
		
			y = ClampAngle (y, yMinLimit, yMaxLimit);
			
			transform.rotation = Quaternion.Euler (y, x, 0);
			transform.position = (Quaternion.Euler (y, x, 0)) * new Vector3 (0.0f, 0.0f, -distance) + orbitTarget.position;
		}
	}

	void DefaultPosition ()
	{
		if (Input.GetKey (KeyCode.Space)) 
		{
			orbitTarget = GameObject.FindGameObjectWithTag("Sun").transform;
			transform.rotation = Quaternion.Euler (90, 0, 0);
			transform.position = transform.position = new Vector3 (0, 121, 0);
		}
	}

	void SelectTarget ()
	{
		Ray ray = new Ray();
		RaycastHit hit = new RaycastHit();
		bool isColided = false;

		if(Input.GetKey(KeyCode.Mouse0)){
			ray = camera.ScreenPointToRay(Input.mousePosition);
			isColided = Physics.Raycast(ray,out hit);
			if(isColided){
				orbitTarget = hit.collider.gameObject.transform;
				OrbitTarget = hit.collider.gameObject;
				transform.LookAt(orbitTarget);
			}
		}
	}

	Vector3 VectDif (Vector3 begin, Vector3 End)
	{
		return new Vector3 (begin.x - End.x, begin.y - End.y, begin.z - End.z);
	}
}