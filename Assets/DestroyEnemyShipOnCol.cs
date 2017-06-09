using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyShipOnCol : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		if(other.tag == "Enemy"){
		//Debug.Log("EnemyDetected");
			if(other.name == "Enemy_Shooter" || other.name == "Enemy_Sucker"){
				other.gameObject.SetActive(false);
			}
			else{
				Destroy(other.gameObject);
			}
		}

	}
}
