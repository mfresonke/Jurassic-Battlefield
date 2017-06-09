using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPuppeteerTailRotation : MonoBehaviour {

	public GameObject puppeteerTail;
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = puppeteerTail.transform.localPosition;
		this.transform.localRotation = puppeteerTail.transform.localRotation;
	}
}
