using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public GameObject enemyShooter; // enemy shooter
	public GameObject enemySucker; // enemy sucker
	public GameObject ship; // player ship
	public float spawnRate; // how often to spawn enemy
	public float startWait; // how long to wait before initially spawning
	public float percentShooter; // what percent of enemies are shooters

	// seven spawning possibilities in semi-circle around player
	private Vector3[] spawnPossibilities = new Vector3[10]; // possible locations to spawn
	private Vector3 spawnPosition; // location of current enemy spawn
	private float nextSpawn; // what time to spawn next enemy

	private int spawnLocation = 0; // tracking for filtered randomness

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
		spawnPossibilities[0] = new Vector3(-55,0,-20);
		spawnPossibilities[1] = new Vector3(-55,0,-10);
		spawnPossibilities[2] =	new Vector3(-55,0,0);
		spawnPossibilities[3] = new Vector3(-55,0,10);
		spawnPossibilities[4] = new Vector3(-55,0,20);
		spawnPossibilities[5] =	new Vector3(55,0,-20);
		spawnPossibilities[6] = new Vector3(55,0,-10);
		spawnPossibilities[7] = new Vector3(55,0,0);
		spawnPossibilities[8] =	new Vector3(55,0,10);
		spawnPossibilities[9] =	new Vector3(55,0,20);

		while(true){
			
			// get player position
			Vector3 playerPosition = ship.transform.position;
			//GameObject.FindGameObjectWithTag ("ShipForNav_middle").transform.position;

			// get random value in spawn possibilities
			int rand = Random.Range (0, 10);
			if (rand == spawnLocation)
				while (rand == spawnLocation)
					rand = Random.Range (0, 10);
			spawnLocation = rand;
			spawnPosition = playerPosition + spawnPossibilities [rand];

			// set rotation of enemy
			Quaternion spawnRotation = Quaternion.identity;
			//spawnRotation.y = 180;

			float rand2 = Random.Range (0.0f, 1.0f);
			if (rand2 <= percentShooter/100) {
				// spawn enemy shooter!
				GameObject clone = Instantiate (enemyShooter, spawnPosition, spawnRotation);
				clone.SetActive(true);
				//Debug.Log ("enemy shooter spawned!!");
			}
			else {
				// spawn enemy sucker!
				GameObject clone = Instantiate (enemySucker, spawnPosition, spawnRotation);
				clone.SetActive(true);
				//Debug.Log ("enemy sucker spawned!!");
			}
				
			// set time for next spawn
			// only spawn enemy after specified time interval
			yield return new WaitForSeconds (spawnRate);
		}
	}
}