using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trajectory : MonoBehaviour {

	GameObject StarDestroyer;
	GameObject trajectory;
	public List<Vector3> pointslist;
	LineRenderer lnrndr;

	public float countofpoints = 0;

	// Use this for initialization
	void Start () {
		StarDestroyer = GameObject.Find("StarDestroyer");
		trajectory = GameObject.Find("Trajectory");
		pointslist = new List<Vector3> ();
		lnrndr = trajectory.GetComponent<LineRenderer> ();
		lnrndr.SetColors (Color.blue, Color.red);
		lnrndr.SetWidth (0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (StarDestroyer.GetComponent<SpaceShipPhysics>().SdLaunched){
			if (countofpoints < 2) {
				countofpoints++;
				pointslist.Add(StarDestroyer.transform.position);
			}
			else
			{
				pointslist.Add(StarDestroyer.transform.position);
	
				lnrndr.SetVertexCount(pointslist.Count);
	
				for (int i = 0; i < pointslist.Count; i++)
					lnrndr.SetPosition(i, pointslist[i]);
			}
		}
	}
}
