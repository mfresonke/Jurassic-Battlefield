using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour { 
	public int maxHealth = 100;
	public int curHealth = 100;
	public GameObject ship;

	public GameObject HealthBar;
	public GameObject HealthBarGreen;
	private Vector3 offset;
	private Vector3 defaultSize;

	void Start () {
		offset = HealthBar.transform.position - transform.position;
		defaultSize = HealthBar.transform.localScale;
	}

	void Update () {
		AdjustCurrentHealth(0);
		if (curHealth == maxHealth) {
			HealthBar.transform.localScale = new Vector3 (0, 0, 0);
		} else {
			HealthBar.transform.localScale = defaultSize;
		}
		if (!ship.activeSelf) {
			SceneManager.LoadScene ("LEVEL1");
		}
	}

	void LateUpdate () 
	{
		HealthBar.transform.position = transform.position + offset;
	}

	public void AdjustCurrentHealth(int adj) {
		curHealth += adj;

		if (curHealth == 0){
			//Destroy (HealthBar);
			HealthBar.SetActive (false);
			//SceneManager.LoadScene("LEVEL1");
		}

		if(curHealth < 0){
			curHealth = 0;
			//SceneManager.LoadScene("LEVEL1");
		}

		if(curHealth > maxHealth)
			curHealth = maxHealth;

		if(maxHealth < 1)
			maxHealth = 1;
	}
}