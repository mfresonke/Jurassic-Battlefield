using UnityEngine;
using System.Collections;

public class Done_AIFireWeapon : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire = 0;

//	void OnTriggerStay (Collider other) 
	void Update ()
	{
		if (Time.time > nextFire) 
		{
			Debug.Log("Enemy AI Shot!");
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}
}