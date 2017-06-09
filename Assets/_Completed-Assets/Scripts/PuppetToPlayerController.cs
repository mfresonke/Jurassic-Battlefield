using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetToPlayerController : MonoBehaviour {
public GameObject PlayerPuppeteer;
public GameObject Player;



	void Update () {
		transform.localPosition = PlayerPuppeteer.transform.localPosition;
        transform.localRotation = PlayerPuppeteer.transform.localRotation;
    }
}
