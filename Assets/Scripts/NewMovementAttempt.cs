using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotDone_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class NewMovementAttempt : MonoBehaviour
{
	public float speed;
	public float radiusXY;
	public GameObject bigShip;
	public NotDone_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float xMax;
	private float zMax;
	//private float nextFire;
	
	//void Update ()
	//{
/*		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
*/	//}

	void FixedUpdate ()
	{
		Vector3 circleCenter = new Vector3(bigShip.GetComponent<Rigidbody>().position.x, 0, bigShip.GetComponent<Rigidbody>().position.z);
		Vector3 radius = new  Vector3(radiusXY, 0, radiusXY);
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float currentCenter = (Mathf.Sqrt(Mathf.Pow((bigShip.transform.position.x),2) + Mathf.Pow((bigShip.transform.position.z),2)));
		//float currentCenterX = bigShip.GetComponent<Rigidbody>().position.x;
		//float currentCenterZ = bigShip.GetComponent<Rigidbody>().position.z;
		float sqrtOfPlayerXY = (Mathf.Sqrt(Mathf.Pow((transform.position.x),2) + Mathf.Pow((transform.position.z),2)));



		Vector3 offset = transform.position - circleCenter;
		offset.Normalize();
		offset = Vector3.Scale(radius, offset);
		if(/*currentCenter+*/ sqrtOfPlayerXY == radiusXY){
		//transform.position = offset;
		xMax = GetComponent<Rigidbody>().position.x;
		zMax = GetComponent<Rigidbody>().position.z;

		}
		else if(/*currentCenter +*/ sqrtOfPlayerXY > radiusXY){
		Debug.Log("Beyond the bound");
		}
		else{
		xMax = radiusXY;
		zMax = radiusXY;
		}
		GetComponent<Rigidbody>().position = new Vector3
		(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x,currentCenter, /*currentCenterX +*/ xMax),
				0.0f,
				Mathf.Clamp(GetComponent<Rigidbody>().position.z,currentCenter, /*currentCenterZ +*/ zMax)
		) ;

		//if((Mathf.Sqrt(Mathf.Pow((bigShip.transform.position.x),2) + Mathf.Pow((bigShip.transform.position.z),2)))+(Mathf.Sqrt(Mathf.Pow((transform.position.x),2) + Mathf.Pow((transform.position.z),2))) < radiusXY){
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		//}
//		GetComponent<Rigidbody>().position = new Vector3
//		(
//			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
//			0.0f, 
//			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
//		);
	}
}
