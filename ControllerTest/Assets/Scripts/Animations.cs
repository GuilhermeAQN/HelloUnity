using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour{

    private Animator anim;
    private PlayerController player;
    private Collisions coll;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start(){
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
        coll = GetComponent<Collisions>();
        sr = GetComponent<SpriteRenderer>();
    } 

    void Update(){
        
    }

    public void Flip(float dirX){
        if(player.wallSlide){
            if(dirX == -1 && sr.flipX)
                return;

            if(dirX == 1 && !sr.flipX)
                return;
        }

        bool side = (dirX == 1) ? false : true;
        sr.flipX = side;
    }
}