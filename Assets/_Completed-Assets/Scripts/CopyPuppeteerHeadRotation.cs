using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPuppeteerHeadRotation : MonoBehaviour {

	public GameObject puppeteerHead;
	public GameObject realHead;
	void Update () {
		realHead.transform.localPosition = puppeteerHead.transform.localPosition;
		realHead.transform.localRotation = puppeteerHead.transform.localRotation;
	}
}
