using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public Player Player;
    public float CameraDistance = 15;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: There's gotta be a better way of doing this
        transform.position = new Vector3(Player.transform.position.x - CameraDistance, Player.transform.position.y + CameraDistance * 0.9f, Player.transform.position.z - CameraDistance);
    }
}
