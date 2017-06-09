using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvl2OnCollision : MonoBehaviour {

public Scene Level1;
public Scene Level2;
public Scene Level3;


	void OnTriggerEnter(Collider other){
		if(other.tag == "Ship"){
		Debug.Log("Time to change scenes!");
			if(SceneManager.GetActiveScene().name == "LEVEL1"){
			Debug.Log("Go to level 2!");
				SceneManager.LoadScene("LEVEL2");

			}
			else if(SceneManager.GetActiveScene().name == "LEVEL2"){
				Debug.Log("Go to level3!");
				SceneManager.LoadScene("LEVEL3");
			}
		}
	}
}
