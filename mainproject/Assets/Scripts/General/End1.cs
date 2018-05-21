using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End1 : MonoBehaviour {

    GameObject player;
    GameObject engine;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        engine = GameObject.FindGameObjectWithTag("Engine");
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        OwnRigid playerBody = player.GetComponent<OwnRigid>();
        OwnRigid end = gameObject.GetComponent<OwnRigid>();
        Phisics pEngine = engine.GetComponent<Phisics>();


        if (AABBvsAABB(playerBody.boundary, end.boundary))
        {
            if (pEngine.count > 5)
            {
                pEngine.message.gameObject.SetActive(true);
                pEngine.next.gameObject.SetActive(true);
            }
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
