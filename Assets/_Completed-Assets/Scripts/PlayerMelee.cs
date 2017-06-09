using UnityEngine;
using System.Collections;

public class PlayerMelee : MonoBehaviour { 
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
	private Health eh;

	void Start() {
		attackTimer = 0;
		coolDown = 1.0f;
		damage = -34;
	}

	void Update() {
		// Find enemy
		target = FindClosestEnemy();
		if (target != null) {
			// Find enemy health
			eh = (Health)target.GetComponent ("Health");
			if (eh != null) {
				// Find enemy health bar (green part)
				targetHealthBar = eh.HealthBarGreen.transform;
				// Find distance to enemy
				distance = Vector3.Distance (target.transform.position, transform.position);

				if (attackTimer > 0)
					attackTimer -= Time.deltaTime;

				if (attackTimer < 0)
					attackTimer = 0;

				if (distance < 2.5f) {
					if (InputManager.GetAttackButton (this.name)) {
						if (attackTimer == 0) {
							Attack ();
							attackTimer = coolDown;
						}
					}
				}
			}
		}
	}

	private void Attack() {
		dir = (target.transform.position - transform.position).normalized;
		direction = Vector3.Dot(dir, transform.forward);

		Debug.Log("Distance to Enemy: " + distance);
		Debug.Log("Direction to Enemy: " + direction);

		if (distance < 2.5f) {
			if (direction > 0) {
				// Adjust enemy health
				eh.AdjustCurrentHealth (damage);
				// Adjust enemy health bar
				float ratio = damage / eh.maxHealth;
				targetHealthBar.transform.localScale -= new Vector3 (0.34f,0,0);
				// Knockback (maybe later)
				//target.GetComponent<Rigidbody>().AddForce(dir);
				if (eh.curHealth == 0) {
					target.SetActive (false);
				}
			}
		}
	}

	private GameObject FindClosestEnemy() {
		objectList = GameObject.FindGameObjectsWithTag("Enemy");
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