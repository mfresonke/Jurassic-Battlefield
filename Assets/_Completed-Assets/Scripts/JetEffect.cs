using UnityEngine;
using System.Collections;

public class JetEffect : MonoBehaviour
{
	public bool Up;
	public bool UpRight;
	public bool Right;
	public bool DownRight;
	public bool Down;
	public bool DownLeft;
	public bool Left;
	public bool UpLeft;

	private ParticleSystem jet;

	void Start() {
		
	}

	void Update() {

	}

	void OnTriggerStay (Collider other)	{
		if (InputManager.GetUseButton(other.gameObject.name)) 
		{
			if (Up) {
				jet = GameObject.Find("JetFlare_MoveUp_R").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetFlare_MoveUp_L").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveUp_R").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveUp_L").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (UpRight) {
				jet = GameObject.Find("JetFlare_MoveUpRight").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveUpRight").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (Right) {
				jet = GameObject.Find("JetFlare_MoveRight_U").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetFlare_MoveRight_D").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveRight_U").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveRight_D").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (DownRight) {
				jet = GameObject.Find("JetFlare_MoveDownRight").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveDownRight").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (Down) {
				jet = GameObject.Find("JetFlare_MoveDown_L").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetFlare_MoveDown_R").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveDown_L").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveDown_R").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (DownLeft) {
				jet = GameObject.Find("JetFlare_MoveDownLeft").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveDownLeft").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (Left) {
				jet = GameObject.Find("JetFlare_MoveLeft_U").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetFlare_MoveLeft_D").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveLeft_U").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveLeft_D").GetComponent<ParticleSystem>();
				jet.Play();
			}
			if (UpLeft) {
				jet = GameObject.Find("JetFlare_MoveUpLeft").GetComponent<ParticleSystem>();
				jet.Play();
				jet = GameObject.Find("JetCore_MoveUpLeft").GetComponent<ParticleSystem>();
				jet.Play();
			}
		}
	}
}


