using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the node class, attached to all the nodes in the scene
public class Nodes : MonoBehaviour {

    public Vector3 position;
    public float Gvalue;
    public float Hvalue;
    public float total;

    public string Special;

    public bool solid = false;

    public Nodes parent;
    public Color nodeC;
    SpriteRenderer renderer;
    
	// Use this for initialization
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //total = Gvalue + Hvalue;
        renderer.color = nodeC;

        position = transform.position;

	}
}
