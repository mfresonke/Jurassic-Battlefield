using UnityEngine;
using System.Collections;

public class DamageOnContact : MonoBehaviour
{
	public int damage;

	private GameObject target;
	private Transform targetHealthBar;

	void Start ()
	{
		//
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Ship")
		{
			return;
		}

		target = GameObject.FindWithTag ("Ship");
		// Find player health
		Health sh = (Health)target.GetComponent ("Health");
		//Debug.Log("Ship Current Health: " + sh.curHealth);
		targetHealthBar = GameObject.FindWithTag ("ShipHealth").transform;
		// Adjust found player health
		//Debug.Log("Adjust ship health with: " + damage);
		sh.AdjustCurrentHealth (damage);
		//Debug.Log("Ship Current Health after adjust: " + sh.curHealth);
		float ratio = damage / sh.maxHealth;

		if (this.tag == "Enemy Bullet") {
			targetHealthBar.transform.localScale -= new Vector3 (0.05f, 0, 0);
		}
		else if (this.tag == "Enemy") {
			targetHealthBar.transform.localScale -= new Vector3 (0.1f, 0, 0);
		}
		else {
			targetHealthBar.transform.localScale -= new Vector3 (0.25f, 0, 0);
		}

		//targetHealthBar.transform.localScale -= new Vector3 (0.1F,0,0);
		Destroy (gameObject);

		if (sh.curHealth == 0) {
			target.SetActive (false);
		}
	}
}