using UnityEngine;
using System.Collections;

public class ShipHealth : MonoBehaviour { 
	public int maxHealth = 100;
	public int curHealth = 100;

	public float healthBarLength;

	void Start () {
		healthBarLength = Screen.width / 8;
	}

	void Update () {
		AdjustCurrentHealth(0);
	}

	void OnGUI() {
		GUI.Box(new Rect(10, 50, 100, 20), "Ship Health:");
		GUI.Box(new Rect(110, 50, healthBarLength, 20), curHealth + " / " + maxHealth);
	}

	public void AdjustCurrentHealth(int adj) {
		curHealth += adj;

		//if(curHealth == 0)
	//		Destroy;

		if(curHealth < 0)
			curHealth = 0;

		if(curHealth > maxHealth)
			curHealth = maxHealth;

		if(maxHealth < 1)
			maxHealth = 1;

		healthBarLength = (Screen.width / 8) * (curHealth / (float)maxHealth);
	}
}