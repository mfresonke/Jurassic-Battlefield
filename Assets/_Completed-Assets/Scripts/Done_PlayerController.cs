using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float radiusXY;
	public GameObject bigShip;
	public Done_Boundary boundary;
	public float force;
    public float minRotate;
	public GameObject player;


	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	//private float nextFire;
	
//	void Update ()
//	{
//		if (Input.GetButton("Fire1") && Time.time > nextFire) 
//		{
//			nextFire = Time.time + fireRate;
//			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//			GetComponent<AudioSource>().Play ();
//		}
		
//	}

	void Update ()
	{
		float bigShipX = bigShip.GetComponent<Rigidbody>().position.x;
		float bigShipZ = bigShip.GetComponent<Rigidbody>().position.z;
		Vector3 charPos = transform.position;
		Vector3 bigShipPos = bigShip.transform.position;
		float charX = transform.position.x;
		float charZ = transform.position.z;
		float moveHorizontal = InputManager.GetMovementX(this.name);
		float moveVertical = InputManager.GetMovementY (this.name);
		float charCenterDistance = Mathf.Sqrt(Mathf.Pow(charX - bigShipX,2) + Mathf.Pow(charZ - bigShipZ,2));

		// player movement here
		Vector3 forceDirection = new Vector3 (bigShipX-charX,0,bigShipZ-charZ);
        Vector3 rotation = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
		Vector3 movement;
        if (rotation.magnitude > minRotate)
        {
            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(rotation);
        }
        movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		//Check if they're trying to leave the circle. If so, apply a force inward
		if(charCenterDistance >= radiusXY){
			GetComponent<Rigidbody>().AddForce(force*forceDirection);
		}
	}
}
