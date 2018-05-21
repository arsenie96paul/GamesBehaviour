using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        OwnRigid playerBody = player.GetComponent<OwnRigid>();

        //  X axis of the camera will be always the same as x axis of the player, so we can keep track the player along the map
        transform.position = new Vector3( playerBody.gameObject.transform.position.x, transform.position.y, transform.position.z);


    }
}
