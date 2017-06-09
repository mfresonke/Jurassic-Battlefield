using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Demo : MonoBehaviour {

	public GameObject enemy; // enemy
	public GameObject Player; // player object
	public float spawnRate; // how often to spawn enemy
	public float startWait; // how long to wait before initially spawning

	// seven spawning possibilities in semi-circle around player
	private Vector3[] spawnPossibilities = new Vector3[3];
	private Vector3 spawnPosition; // location of enemy spawn
	private float nextSpawn; // what time to spawn next enemy
	private GameObject enemyClone; // clone of initial enemy

	private int[] spawnLocations = new int[2];

	// Use this for initialization
	void Start () {

		// spawn enemies when game starts
		StartCoroutine (SpawnEnemies ());

	}
		
//	void SpawnEnemies () {
	IEnumerator SpawnEnemies ()
	{
		// start spawning enemies after wait time
		yield return new WaitForSeconds (startWait);

		// 3 possible locations for enemy, relative to player
		spawnPossibilities[0] = new Vector3(-5,0,8);
		spawnPossibilities[1] = new Vector3(5,0,8);
		spawnPossibilities[2] =	new Vector3(0,0,8);

		while(true){

			// get player position
			Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;

			// get random value in spawn possibilities
			int rand = Random.Range (0, 3);
			if (rand == spawnLocations [0] && rand == spawnLocations [1])
				while (rand == spawnLocations [0])
					rand = Random.Range (0, 3);
			spawnLocations [0] = spawnLocations [1];
			spawnLocations [1] = rand;
			spawnPosition = playerPosition + spawnPossibilities [rand];

			// set rotation of enemy
			Quaternion spawnRotation = Quaternion.identity;
			spawnRotation.y = 180;

			// spawn enemy!
			GameObject clone = Instantiate (enemy, spawnPosition, spawnRotation);
			clone.SetActive (true);
			Debug.Log ("enemy spawned!!");

			// set time for next spawn
			// only spawn enemy after specified time interval
			yield return new WaitForSeconds (spawnRate);
		}
	}
}

//	// array to keep previous spawn locations
//	private int[] spawnLocations = new int[2];

//			// filter randomness; no number can be repeated 3 times in a row
//			if (spawnLocations [0] == rand && spawnLocations [1] == rand) {
//				while(rand == spawnLocations[0])
//					rand = Random.Range (0, 3);
//			}
//			spawnLocations [0] = spawnLocations [1];
//			spawnLocations [1] = rand;