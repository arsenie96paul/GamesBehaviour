using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {


    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        OwnRigid playerBody = player.GetComponent<OwnRigid>();
        OwnRigid bomb = gameObject.GetComponent<OwnRigid>();

        // If player body and coin body collide, remove coin and increase score
        if (AABBvsAABB(playerBody.boundary, bomb.boundary))
        {
            playerBody.gameObject.SetActive(false);
        }
    }

    // AABB Check collision
    bool AABBvsAABB(AABB a, AABB b)
    {
        if (a.MAX.x < b.MIN.x || a.MIN.x > b.MAX.x)
            return false;

        if (a.MAX.y < b.MIN.y || a.MIN.y > b.MAX.y)
            return false;

        return true;
    }

}
