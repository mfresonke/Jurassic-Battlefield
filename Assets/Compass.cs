using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Compass : MonoBehaviour {

	public Transform target;
	public Transform ship;

	private Quaternion _lookRotation;
	private Vector3 _direction;
	private float smooth = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//find the vector pointing from our position to the target
		_direction = (target.position - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * smooth);
	}
}
