using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This is my player controller script, it handle the movement of the player
public class PlayerController : MonoBehaviour
{
    // Variables
    public float speed = 5;
    public float JumpSpeed;

    GameObject engine;


    //public Animator anim;

    // Use this for initialization
    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("Engine");      

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get components
        OwnRigid playerBody = GetComponent<OwnRigid>();
        Phisics handler = engine.GetComponent<Phisics>();


        // Left
        if (Input.GetKey(KeyCode.A))
        {
            playerBody.velocity.x += -speed * Time.deltaTime;
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            playerBody.velocity.x += +speed * Time.deltaTime;
        }

        // Release Movement Left or Right
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerBody.velocity.x = 0.0f;
        }

        // if not in air, player can jump
        if (handler.grounded)
        {
            JumpSpeed = 200;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerBody.velocity.y += JumpSpeed * Time.deltaTime;
            }
            if ( JumpSpeed == 200)
            {
                handler.grounded = false;
            }
            if (handler.grounded == false)
            {
                JumpSpeed = 0;
            }
        }
        
    }
}
