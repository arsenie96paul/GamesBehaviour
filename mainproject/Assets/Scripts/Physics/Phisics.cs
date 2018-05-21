using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is the properly physics engine, it handle all the physics in the scene
public class Phisics : MonoBehaviour {

    // Game Objects
    GameObject player;
    GameObject[] stairs;
    GameObject[] pickUps;
    GameObject[] solid;

    // another values
    public PlayerController movement;
    public Vector2 something;

    // Values
    public float gravity = 9.81f;
    public float counter = 0;

    // Bools 
    public bool isGravity = true;
    public bool stairsBool ;
    public bool grounded;

    // position of collision
    public string Coltipe;

    // UI 
    public int count;
    public Text countText;
    public Text message;
    public Button next;

    // Use this for initialization
    void Start() {

        // Find Objects
        player = GameObject.FindGameObjectWithTag("Player");
        solid = GameObject.FindGameObjectsWithTag("Solid");
        pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        stairs = GameObject.FindGameObjectsWithTag("Stairs");

        // UI Start
        count = 0;
        countText.text = "Score: " + count.ToString();
        message.gameObject.SetActive(false);
        next.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the components
        movement = GetComponent<PlayerController>();
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

            // apply colision here
            if (colisionHere )
            {
                Collide(playerBody, s);
            }
        }

        // for each stair, check collision
        foreach (OwnRigid st in stairsBody)
        {
            stairsBool = AABBvsAABB(playerBody.boundary, st.boundary);   
        }
    }

    // Collision
    void Collide(OwnRigid a, OwnRigid b)
    {
        Vector2 normal;         

        // check collision and set normal vector
        if (Coltipe == "down")
        {
            normal = Vector2.down;
        }
        else if (Coltipe == "up")
        {
            normal = Vector2.up;
        }
        else if (Coltipe == "right")
        {
            normal = Vector2.right;
        }
        else if (Coltipe == "left")
        {
            normal = Vector2.left;
        }

        else normal = Vector2.up;

        Vector2 rv = b.velocity - a.velocity;               // Relative velocity from player to object
        float VAN = Vector2.Dot(rv, normal);                // Vector along normal

        if (VAN > 0)  
            return;

        float j = -1.5f * VAN;                              // e = 0.5; it need some bounce 
        j = j / (1 / a.mass + 1 / b.mass);                  // bounce decrease every time 2 object collide

        Vector2 imp = j * normal;                           // calculate impulse along  normal
        Vector3 impulse = new Vector3(imp.x, imp.y, 0.0f);  // Transform impulse from Vector2 to Vector3
        a.velocity -= 1 / a.mass * impulse;                 // apply the impulse 

        //something = impulse;                                // DISPLAY IMPULSE ( just for test )

        // manypulate velocity for each case of collision
        if (impulse.y > -4.0)                              // if bounce stop
            if (Coltipe == "down")
            {
                a.velocity.y = gravity * Time.deltaTime;
            }
            else if (Coltipe == "right")
            {
                a.velocity = Vector3.left * movement.speed * Time.deltaTime;
            }
            else if (Coltipe == "left")
            {
                a.velocity = Vector3.left * (-movement.speed) * Time.deltaTime;
            }
    }

    // AABB Check collision
    bool AABBvsAABB(AABB a, AABB b)
    {
        // ----- properly aabb colision test
        // check collision
        if (a.MAX.x < b.MIN.x || a.MIN.x > b.MAX.x)
            return false;

        if (a.MAX.y < b.MIN.y || a.MIN.y > b.MAX.y)
            return false; 

        //------- additional code, not necessary

        // if collsion, set collision tipe
        if ((a.MAX.x > b.MIN.x && a.MIN.x < b.MAX.x) && (a.MAX.y > b.MIN.y && a.MIN.y < b.MAX.y))
        {
            Coltipe = "down";
            grounded = true;
        }
        if ((a.MAX.x > b.MIN.x && a.MIN.x < b.MIN.x) && (a.MAX.x > b.MIN.x || a.MIN.x < b.MAX.x))
        {
            Coltipe = "right";
        }
        if ((a.MAX.x > b.MAX.x && a.MIN.x < b.MAX.x) && (a.MAX.x > b.MIN.x || a.MIN.x < b.MAX.x))
        {
            Coltipe = "left";
        }

        return true;
    }

    // Gravity 
    public void Gravity(OwnRigid a)
    {
        a.velocity.y += -gravity * Time.deltaTime;
    }
}