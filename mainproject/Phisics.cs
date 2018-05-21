using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phisics : MonoBehaviour {

    // Game Objects
    GameObject player;
    GameObject[] stairs;
    GameObject[] pickUps;
    GameObject[] solid;

    public Vector2 something;

    // Values
    public float gravity = 9.81f;
    public int counter = 0;

    // Bools 
    public bool isGravity = true;
    public bool stairsBool = false;

    // Use this for initialization
    void Start () {

        // Find Objects
        player = GameObject.FindGameObjectWithTag("Player");
        solid = GameObject.FindGameObjectsWithTag("Solid");
        pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        stairs = GameObject.FindGameObjectsWithTag("Stairs");

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the components
        PlayerController movement = GetComponent<PlayerController>();
        OwnRigid playerBody = player.GetComponent<OwnRigid>();
        OwnRigid[] solidBody = new OwnRigid[solid.Length];
        OwnRigid[] stairsBody = new OwnRigid[stairs.Length];

        for (int i = 0; i < solid.Length; i++)
        {
            solidBody[i] = solid[i].GetComponent<OwnRigid>();
        }

        for (int i = 0; i < stairs.Length; i++)
        {
            stairsBody[i] = stairs[i].GetComponent<OwnRigid>();
        }


        // Create gravity for player
        if (isGravity)
        {
            Gravity(playerBody);
        }

        //float counter = 0;

        // check collision and stop the gravity
        foreach (OwnRigid s in solidBody)
        {
            bool colisionHere;
            colisionHere = AABBvsAABB(playerBody.boundary, s.boundary);

            if (colisionHere)
            {
                Collide(playerBody, s);
            }
        }

        foreach (OwnRigid st in stairsBody)
        {
            stairsBool = AABBvsAABB(playerBody.boundary, st.boundary);
        }
    }

    // Collision
    void Collide (OwnRigid a, OwnRigid b)
    {
        Vector2 normal = Vector2.down;                      // Normal vector
        Vector2 rv = b.velocity - a.velocity;               // Relative velocity from player to object
        float VAN = Vector2.Dot(rv, normal);                // Vector along normal

        if (VAN > 0)  // <--- Bounce ( commenting will stop the bounce )
           return;

        float j =  - 2.0f*VAN;                              // e = 1; it need some bounce 
        j = j / (1 / a.mass + 1 / b.mass);                  // bounce increase every time 2 object collide

        Vector2 imp = j * normal;                           // calculate impulse along  normal
        Vector3 impulse = new Vector3(imp.x, imp.y, 0.0f);  // Transform impulse from Vector2 to Vector3
        a.velocity -= 1 / a.mass * impulse;                 // apply the impulse 

        something = impulse;                                // DISPLAY IMPULSE ( just for test )
	
		//if (impulse.x == 0.0f)
        if (impulse.y > -2.5 )                              // if bounce stop
            a.velocity.y = gravity * Time.deltaTime;        // keep the player above the grond
			//a.velocity = -a.velocity;
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

    // Gravity 
    public void Gravity(OwnRigid a)
    {
        a.velocity.y += -gravity * Time.deltaTime;
        a.gameObject.transform.position += a.velocity * Time.deltaTime;
    }
}