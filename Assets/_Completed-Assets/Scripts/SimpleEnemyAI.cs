using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : MonoBehaviour { 
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;

	private float distance;
	private Vector3 position;
	private GameObject[] objectList;
	private GameObject closest;
	private Transform myTransform;
	private Transform target;

	void Awake () {
		myTransform = transform;
	}

	void Start () {
		moveSpeed = 1;
		rotationSpeed = 1;
		maxDistance = 2;
	}

	void Update () {
		target = FindClosestPlayer ().transform;
		Debug.DrawLine (target.position, myTransform.position, Color.yellow);

		// Look at target
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);

		if (Vector3.Distance (target.position, myTransform.position) > maxDistance) {
			// Move towards target
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}

	private GameObject FindClosestPlayer() {
		objectList = GameObject.FindGameObjectsWithTag("Player");
		closest = null;
		distance = Mathf.Infinity;
		position = transform.position;

		foreach (GameObject gameobject in objectList) {
			Vector3 diff = gameobject.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = gameobject;
				distance = curDistance;
			}
		}
		return closest;
	}
}