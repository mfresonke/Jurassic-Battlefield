using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPuppeteerTurretRotation : MonoBehaviour {
	public GameObject PuppeteerTurret;

	
	void Update () {
		transform.rotation = PuppeteerTurret.transform.rotation;
	}
}
