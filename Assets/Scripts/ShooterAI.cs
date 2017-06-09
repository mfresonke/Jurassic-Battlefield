using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : MonoBehaviour {

	public Transform targetTop;
	public Transform targetMiddle;
	public Transform targetBottom;
	public Transform offset; // ?
	NavMeshAgent agent;
	public GameObject shot;
	public Transform shotSpawn;
	public float speed; // max speed of enemy
	public float initialWaitTime; // how long to wait after spawning to begin
	public float stopRate; // how often enemy stops to shoot
	public float stopDuration; // how long to stop to shoot
	public float fireRate; // fire rate
	public float directionChangeRate; // how long to head in one direction before changing

	private Transform target;
	private float nextStop; // time to stop next
	private float nextFire; // time to fire next
	private float nextDirectionChange; // time to change directions
	private Quaternion _lookRotation;
	private Vector3 _direction;
	private int minDistance = 24; // min distance between enemy and ship

	// Use this for initialization
	void Start () {

		agent = GetComponent<NavMeshAgent> ();

		target = targetMiddle;
		initialWaitTime += Time.time;
	}

	// Update is called once per frame
	void Update () {

		if(Time.time < initialWaitTime){
			agent.SetDestination (target.position);
		}
		else{
			// change destination
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

			if (Time.time > nextStop) {
				if (Time.time < nextStop + stopDuration) {
					if (Time.time > nextFire) {
						nextFire = Time.time + Random.Range (fireRate - fireRate / 3, fireRate + fireRate / 3);
						Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
						GetComponent<AudioSource> ().Play ();
						agent.speed = 0;
					}
					//find the vector pointing from our position to the target
					_direction = (target.position - transform.position).normalized;

					//create the rotation we need to be in to look at the target
					_lookRotation = Quaternion.LookRotation(_direction);

					//rotate us over time according to speed until we are in the required rotation
					transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * agent.angularSpeed);
				} 
				else {
					nextStop = Time.time + stopRate;
					agent.speed = speed;
				}
			}

			// check if enemy is too close to ship
			if(Vector3.Distance(transform.position, targetMiddle.position) <= minDistance){
				if(targetMiddle.position.x > transform.position.x){
					if(targetMiddle.position.z > transform.position.z){
						agent.SetDestination(new Vector3(targetMiddle.position.x-70, 0, targetMiddle.position.z-70));
					}
					else if(targetMiddle.position.z <= transform.position.z){
						agent.SetDestination(new Vector3(targetMiddle.position.x-70, 0, targetMiddle.position.z+70));
					}
				}
				else if(targetMiddle.position.x <= transform.position.x){
					if(targetMiddle.position.z > transform.position.z){
						agent.SetDestination(new Vector3(targetMiddle.position.x+70, 0, targetMiddle.position.z-70));
					}
					else if(targetMiddle.position.z <= transform.position.z){
						agent.SetDestination(new Vector3(targetMiddle.position.x+70, 0, targetMiddle.position.z+70));
					}
				}
				agent.speed = speed;
				//find the vector pointing from our position to the target
				_direction = (target.position - transform.position).normalized;

				//create the rotation we need to be in to look at the target
				_lookRotation = Quaternion.LookRotation(_direction);

				//rotate us over time according to speed until we are in the required rotation
				transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * agent.angularSpeed);
			}
			else{
				agent.SetDestination (target.position);
				agent.speed = speed;
			}
		}

	}

	void OnTriggerEnter (Collider other) {
		if (other.name == "Done_Bolt(Clone)") {
			Debug.Log ("killed by " + other.name);

			Destroy (other.gameObject);
			// if object is original enemy shooter, do not destroy
			if (gameObject.name == "Enemy_Shooter")
				gameObject.SetActive (false);
			else
				Destroy (gameObject);
		}
		else if(other.name == "Ship"){
			if (gameObject.name == "Enemy_Shooter")
				gameObject.SetActive (false);
			else
				Destroy (gameObject);
		}
	}
}
