using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckerAttach : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if(other.name == "Enemy_Sucker" || other.name == "Enemy_Sucker(Clone)") {
        	other.transform.parent = transform;
        	Debug.Log("enemy sucker collided");
    	}
    	else return;
		
	}
}
