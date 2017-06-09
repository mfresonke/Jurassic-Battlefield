using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StationRotation : MonoBehaviour {
    public float rotationSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 axis = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //transform.Rotate(Vector3.right * 50 * Time.deltaTime); //rotates 50 degrees per second around y (but really x) axis

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ship")
        {
            SceneManager.LoadScene("LEVEL2");
        }
    }
}
