using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerManager : MonoBehaviour{

    private Animations anim;
    [HideInInspector]
    public Collisions coll;
    [HideInInspector]
    public Rigidbody2D rb;
    
    public bool canMove;
    public bool wallJumped;

    public float dirX = 1;

    void Awake(){
        
    }

    void FixedUpdate(){
        
    }


}