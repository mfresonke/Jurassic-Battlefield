using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float radiusXY;
	public float circleCenterXY;
	

	void FixedUpdate (){
		Vector3 circleCenter = new Vector3(circleCenterXY, 0, circleCenterXY);
		Vector3 radius = new  Vector3(radiusXY, 0, radiusXY);
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		Vector3 offset = transform.position - circleCenter;
		offset.Normalize();
		offset = Vector3.Scale(radius, offset);
		if((Mathf.Sqrt(Mathf.Pow((GetComponent<Rigidbody>().position.x),2) + Mathf.Pow((GetComponent<Rigidbody>().position.z),2))) > radiusXY){
		GetComponent<Rigidbody>().position = offset;
		}
	}

}