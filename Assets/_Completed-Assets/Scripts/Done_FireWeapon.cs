using UnityEngine;
using System.Collections;

public class Done_FireWeapon : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public GameObject gunFunnel;
	public bool RightSide;
	public GameObject aim;
	public float aimSpeed;
	public int rotationSpeed;

	private float nextFire;
	
	void OnTriggerStay (Collider other) 
	{

		if (InputManager.GetShootButton(other.gameObject.name) && Time.time > nextFire) 
		{
		//Debug.Log("Hit the A Button!");
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
		float rotateVertical = InputManager.GetAimY(other.gameObject.name);

		Vector3 rotation = new Vector3 (0.0f, 0.0f, rotateVertical);
		if(RightSide){
			
			aim.GetComponent<Rigidbody>().velocity = rotation * aimSpeed;
		}
		else{
			aim.GetComponent<Rigidbody>().velocity = rotation * aimSpeed;
		}
		Vector3 Down = new Vector3(0,0,-10);
		Vector3 Up = new Vector3(0,0,10);
		if(RightSide){
			if(aim.transform.position.z < -1.5){
				aim.GetComponent<Rigidbody>().AddForce(50*Up);
			}
			if(aim.transform.position.z > 1.3){
				aim.GetComponent<Rigidbody>().AddForce(50*Down);
			}
		}
		else{
			if(aim.transform.position.z < -1){
				aim.GetComponent<Rigidbody>().AddForce(50*Up);
			}
			if(aim.transform.position.z > 1.3){
				aim.GetComponent<Rigidbody>().AddForce(50*Down);
			}
		}





	}
	void Update(){
		if(RightSide){
			Vector3 pointToLook = new Vector3(aim.transform.position.x-20,0.0f,aim.transform.position.z/10);
			gunFunnel.transform.LookAt(pointToLook);
			}
			else{
			Vector3 pointToLook = new Vector3(aim.transform.position.x+20,0.0f,aim.transform.position.z/10);
			gunFunnel.transform.LookAt(pointToLook);
			}
	
	}
}