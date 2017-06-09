using UnityEngine;
using System.Collections;

public class Done_MoveShip : MonoBehaviour
{
    public GameObject ship;
 	public static int movespeed = 2;
 	public Vector3 userDirection = Vector3.right;
 	public GameObject player;
	public Vector3 shipPosition;
	public Vector3 playerPosition; 
	public Vector3 shipPlayerDifference = new Vector3 (0,0,0);
	Vector3 tempShipLoc = new Vector3 (0,0,0);
	public int shipSpeed;
	public GameObject player1;

	void Start(){
	player.transform.parent = ship.transform;
	shipSpeed = 800;
	}

	void OnTriggerStay (Collider other) 
	{			
		if (InputManager.GetUseButton(other.gameObject.name)) 
		{
			ship.GetComponent<Rigidbody>().AddForce(shipSpeed*userDirection*movespeed*Time.deltaTime);
		}
	}
	void Update() {
//		shipPlayerDifference = ship.transform.position - tempShipLoc;
//			tempShipLoc = ship.transform.position;
//			player.transform.localPosition = player.transform.localPosition + shipPlayerDifference - new Vector3 (0, shipPlayerDifference.y, 0);
	 } 
}