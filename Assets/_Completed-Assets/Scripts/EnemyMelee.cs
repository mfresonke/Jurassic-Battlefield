using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour { 
	public float attackTimer;
	public float coolDown;
	public int damage;

	private GameObject target;
	private GameObject[] objectList;
	private GameObject closest;
	private float distance;
	private float direction;
	private Vector3 position;
	private Vector3 dir;
	private Transform targetHealthBar;
	private Health ph;

	void Start() {
		attackTimer = 0;
		coolDown = 2.0f;
		damage = -25;
	}

	void Update() {
		// Find player
		target = FindClosestPlayer();
		if (target != null) {
			// Find player health
			ph = (Health)target.GetComponent ("Health");
			if (ph != null) {
				// Find player health bar (green part)
				targetHealthBar = ph.HealthBarGreen.transform;
				// Find distance to player
				distance = Vector3.Distance (target.transform.position, transform.position);

				if (attackTimer > 0)
					attackTimer -= Time.deltaTime;

				if (attackTimer < 0)
					attackTimer = 0;

				if (distance < 2.5f) {
					if (attackTimer == 0) {
						Attack ();
						attackTimer = coolDown;
					}
				}
			}
		}
	}

	private void Attack() {
		dir = (target.transform.position - transform.position).normalized;
		direction = Vector3.Dot(dir, transform.forward);



		if (distance < 2.5f) {
			if (direction > 0) {
				// Find player health
				ph = (Health)target.GetComponent ("Health");
				// Adjust player health
				ph.AdjustCurrentHealth (damage);
				// Adjust player health bar
				float ratio = damage / ph.maxHealth;
				targetHealthBar.transform.localScale -= new Vector3 (0.25f,0,0);
				//targetHealthBar.transform.localScale -= new Vector3 (0.1F,0,0);
				// Knockback (maybe later)
				//target.GetComponent<Rigidbody>().AddForce(dir);
				if (ph.curHealth == 0) {
					target.SetActive (false);
				}
			}
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

