using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

    GameObject player;
    GameObject engine;
   
    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        engine = GameObject.FindGameObjectWithTag("Engine");
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Take bodies
        OwnRigid playerBody = player.GetComponent<OwnRigid>();
        OwnRigid pickBody = gameObject.GetComponent<OwnRigid>();
        Phisics pEngine = engine.GetComponent<Phisics>();

        // If player body and coin body collide, remove coin and increase score
        if (AABBvsAABB(playerBody.boundary, pickBody.boundary) )
        {
            gameObject.SetActive(false);
            pEngine.count++;
            pEngine.countText.text = "Score: " + pEngine.count.ToString();
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
