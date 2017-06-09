using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurTailRotation : MonoBehaviour {

	public GameObject Tail;
	public GameObject aim;
	public float aimSpeed;
	public int rotationSpeed;
	
	void OnTriggerStay (Collider other) 
	{
		string playerName = other.gameObject.name;
		float rotateHorizontal = InputManager.GetAimX (playerName);
		Vector3 rotation = new Vector3 (rotateHorizontal, 0.0f, 0.0f);
		if (InputManager.GetShootButton(playerName)) {
			aim.GetComponent<Rigidbody>().velocity = rotation * aimSpeed;
		}

		Vector3 Left = new Vector3(-10,0,0);
		Vector3 Right = new Vector3(10,0,0);
			if(aim.transform.localPosition.x >= 23){
				aim.GetComponent<Rigidbody>().AddForce(30*Right);
			}
			if(aim.transform.localPosition.x <= -23){
				aim.GetComponent<Rigidbody>().AddForce(30*Left);
			}

	}
	void Update(){
		
			Vector3 pointToLook = new Vector3(aim.transform.position.x,0.0f,aim.transform.position.z/10);
			Tail.transform.LookAt(pointToLook);
	}
}