using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuckerAI : MonoBehaviour {

	public Transform targetTop;
	public Transform targetMiddle;
	public Transform targetBottom;
	public Transform offset;
	public Transform ship;
	NavMeshAgent agent;
	public float directionChangeRate; // how long to head in one direction before changing
	public GameObject enemy_insect; // enemy insect

	private Transform target;
	private float nextDirectionChange; // time to change directions
	private double onEdge = 12; // distance to center of ship to help stop enemy when on edge of ship
	private bool attached = false; // is enemy attached to ship
	private Vector3 distanceFromCenter; // distance from center of ship upon attaching
	private float timeAttached = 1.0f; // amount of time sucker is attached to ship before it comes in
	// Use this for initialization
	void Start () {
		
		agent = GetComponent<NavMeshAgent> ();
		target = targetMiddle;
		enemy_insect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(attached == false){

			if(Time.time > nextDirectionChange){
				int rand = Random.Range (0, 3);
				if(rand == 0)
					target = targetTop;
				else if(rand == 1)
					target = targetMiddle;
				else
					target = targetBottom;
				nextDirectionChange = Time.time + directionChangeRate;
			}
			agent.SetDestination (target.position);

			if(Vector3.Distance(transform.position, targetMiddle.position) <= onEdge){
				attached = true;
				agent.speed = 0;
				distanceFromCenter = transform.position - targetMiddle.position;
				timeAttached += Time.time;
			}
		}
		else{
			// destroy and spawn enemy inside ship (after some amount of time?)
			if(Time.time >= timeAttached){
				Spawn_insect ();
				if(gameObject.name != "Enemy_Sucker")
					Destroy(gameObject);
				else
					gameObject.SetActive(false);
			}
			else{
				transform.position = distanceFromCenter + targetMiddle.position;
			}
		}

	}

	void OnTriggerEnter (Collider other) {
		if (other.name != "Done_Bolt(Clone)") {
			return;
		}

		Debug.Log ("killed by " + other.name);

		Destroy (other.gameObject);
		// if object is original enemy shooter, do not destroy
		if (gameObject.name == "Enemy_Sucker")
			gameObject.SetActive (false);
		else
			Destroy (gameObject);
	}
		
	void Spawn_insect ()
	{
		Vector3 offset = new Vector3(0,14,0);
		Vector3 spawnPosition = transform.position + transform.forward*5 + offset;
		Quaternion spawnRotation = Quaternion.identity;
		// spawn enemy insect clone
		GameObject enemy_insect_clone = Instantiate (enemy_insect, spawnPosition, spawnRotation, ship);
		enemy_insect_clone.SetActive (true);
	}
}