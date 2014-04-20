using UnityEngine;
using System.Collections;

public class PlanetMovement : MonoBehaviour {
	//public LineRenderer Orbit;
	public float RadiusVector;
	public float ApogeyDistance = 20f;
	public float Excentricity = 0.25f;
	public float PerehelionAngle = 0f;
	public float ZOrbitAngle = 0f;
	public float EclipticToOrbitAngle = 0f;
	public float Nu;
	public float Speed = 100f;
	//public int vertexCount = 10;

	float t = 0;

	void Start () {
		/*Orbit.SetVertexCount(vertexCount);
		Orbit.SetColors(Color.green,Color.green);
		Orbit.useWorldSpace = false;
*/
		RadiusVector = getRadius(Excentricity,ApogeyDistance,Nu);
	}
	
	void Update () {
		//DrawOrbit(Orbit);
		changeNu(ref Nu,Speed);
		RadiusVector = getRadius(Excentricity,ApogeyDistance,Nu);
		this.transform.position = Move(ZOrbitAngle,PerehelionAngle,RadiusVector,Nu,EclipticToOrbitAngle);
	}

	Vector3 Move (float Omega, float omega, float r, float nu, float i) {
		float x = r * (Mathf.Cos(Omega) * Mathf.Cos(omega + nu) - Mathf.Sin(Omega)*Mathf.Sin(omega + nu)) * Mathf.Cos(i);
		float y = r * (Mathf.Sin(Omega) * Mathf.Cos(omega + nu) + Mathf.Cos(Omega)*Mathf.Sin(omega + nu)) * Mathf.Cos(i);
		float z = r * (Mathf.Sin(omega + nu) * Mathf.Sin(i)); 

		return new Vector3(x,y,z);
	}

	float getRadius (float E, float apo, float nu) {
		float a = apo /(1 + E);
		return ((a * (1 - E * E)) / (1 + E * Mathf.Cos(nu)));
	}

	void changeNu (ref float Nu, float speed){
		if(t >= 1)
			t = 0;
		t = Time.deltaTime / speed;
		Nu = Mathf.Lerp(0,360, t);
	}

	/*void DrawOrbit(LineRenderer line){
		Vector3 point = new Vector3(0,0,0);
		for(int i = 0; i < vertexCount;i++){
			RadiusVector = getRadius(Excentricity,ApogeyDistance,i);
			point = Move(ZOrbitAngle,PerehelionAngle,RadiusVector,i,EclipticToOrbitAngle);
			line.SetPosition(i,point);
		}
	}*/
}
