using UnityEngine;
using System.Collections;

public class PlanetMovement : MonoBehaviour {
	public float StartAngle = 0.0f;
	public Vector3 RotCenterOffset;
	
	public float Dist = 10.0f;
	public float Eks = 0.017f;
	public float DeclAngle = 0.1265f; // Radian
	public float Period = 1.0f;
	public float DeltaTime = 0.001f;

	float Rz, Rx;
	void Start () {
		Rz = GetLargeAxis(Eks, Dist);
		Rx = GetSmallAxis (Eks, Dist);
		RotCenterOffset = GetRotCenterOff (Eks, DeclAngle, Dist);
	}
	
	public float GetLargeAxis(float eks, float dist){
		return dist / (1 + eks);
	}
	public float GetSmallAxis(float eks, float dist){
		return dist * Mathf.Sqrt ((1 - eks) / (1 + eks));
	}
	public Vector3 GetRotCenterOff(float eks, float declAngle, float dist){
		float rz = dist / (1 + eks);
		return new Vector3 (0.0f, (float)(rz*eks*Mathf.Sin(declAngle)),
		                    (float)(rz*eks));
	}

	void Update () {
		float x = Rx * Mathf.Sin (StartAngle) + RotCenterOffset.x;
		float z = Rz * Mathf.Cos (StartAngle) + RotCenterOffset.z;
		float y = RotCenterOffset.y * Mathf.Cos(StartAngle);
		
		this.transform.position = new Vector3 (x, y, z);
		
		if ((StartAngle -= (DeltaTime / Period)) < Mathf.PI * -2)
			StartAngle = 0.0f;
	}
}
