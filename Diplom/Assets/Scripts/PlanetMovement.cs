using UnityEngine;
using System.Collections;

public class PlanetMovement : MonoBehaviour {
	public float StartAngle = 0.0f;
	public Vector3 RadiusVect;
	public Vector3 RotCenterOffset;

	public float declX = 0.0f;
	public float declZ = 0.0f;
	public float declY = 0.0f;

	public float Dist = 10.0f;
	public float Eks = 0.017f;
	public float Period = 1.0f;
	public float DeltaTime = 0.001f;
	
	void Start () {
		RadiusVect = new Vector3 (GetSmallAxis (Eks, Dist), 0, GetLargeAxis (Eks, Dist));
		RotCenterOffset = GetRotCenterOff (Eks, Dist);
	}
	
	public float GetLargeAxis(float eks, float dist){
		return dist / (1 + eks);
	}
	public float GetSmallAxis(float eks, float dist){
		return dist * Mathf.Sqrt ((1 - eks) / (1 + eks));
	}
	public Vector3 GetRotCenterOff(float eks, float dist){
		float rz = dist / (1 + eks);
		return new Vector3 (0.0f, 0.0f,(float)(rz*eks));
	}

	public float[,] Matrix3Mul(float[,] m1, float[,] m2){
		float[,] mres = new float[3, 3];
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++){
				mres[i, j] = Vector3.Dot(new Vector3(m1[i, 0], m1[i, 1], m1[i,2]),
				                        new Vector3(m2[0, j], m2[1, j], m2[2, j]));
			}
		}
		return mres;
	}

	public float[,] MRotX(float angle){
		return new float[,] {
			{1, 0, 0}, 
			{0, Mathf.Cos(angle), -Mathf.Sin(angle)},
			{0, Mathf.Sin(angle), Mathf.Cos(angle)}};
	}
	public float[,] MRotY(float angle){
		return new float[,] {
			{Mathf.Cos(angle), -Mathf.Sin(angle), 0}, 
			{Mathf.Sin(angle), Mathf.Cos(angle), 0},
			{0, 0, 1}};
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

	void Update () {
		float x = RadiusVect.x * Mathf.Sin (StartAngle) + RotCenterOffset.x;
		float z = RadiusVect.z * Mathf.Cos (StartAngle) + RotCenterOffset.z;
		float y = RadiusVect.y * Mathf.Cos (StartAngle) + RotCenterOffset.y;

		this.transform.position = MRotVect (new Vector3 (x, y, z),
		                                   Matrix3Mul (Matrix3Mul (MRotX (declX), MRotY (declZ)), MRotZ (declY)));
		
		if ((StartAngle -= (DeltaTime / Period)) < Mathf.PI * -2)
			StartAngle = 0.0f;
	}
}
