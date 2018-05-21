using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// OwnRigid is my onw RigidBody component, attached to all the object on the scene


public class OwnRigid : MonoBehaviour {

    // Object velocity
    public Vector3  velocity;   
    public AABB boundary;       // Rectangle
    public float mass;
    public string properties;

    // Use this for initialization
    void Start()
    {
        boundary = new AABB();  // Create rectangle for this object
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CreateCollisionBox();
        gameObject.transform.position += velocity * Time.deltaTime;

    }
    
    // Crate for object 
    void CreateCollisionBox()
    {
        float pwidth = GetComponent<Renderer>().bounds.size.x;
        float pheight = GetComponent<Renderer>().bounds.size.y;

        boundary.MIN = new Vector2(transform.position.x - (pwidth / 2), transform.position.y - (pheight / 2));
        boundary.MAX = new Vector2(transform.position.x + (pwidth / 2), transform.position.y + (pheight / 2));
    }
}