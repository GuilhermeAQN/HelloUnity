using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour{

    public PlayerMovement player;
    public Collisions coll;

    public int btnSide;

    public float maxAxis = 0;

    public bool btnPress;
    public bool onWall;
    public bool onLeaveWall;
    public bool inputMobile = true;

    public float x;
    public float y;
    public float dirX = 1;
    public Vector2 dir;

    void Update(){
        if(btnPress == false){
            if(maxAxis != 0){
                maxAxis = Mathf.MoveTowards(maxAxis, 0, 3 * Time.deltaTime);
                x = maxAxis;
            }
        }
    }

    void FixedUpdate(){
        if(onWall && !coll.onWall){
            StartCoroutine(SaiuDaParede());
        }
        onWall = coll.onWall;

        // ---------- true = Mobile input | false = PC input ---------- \\
        if(Input.GetKeyDown(KeyCode.T)){
            if(inputMobile == false){
                inputMobile = true;
                return;
            }else{
                inputMobile = false;
                return;
            }
        }
        if(inputMobile){  // -----> Mobile input
            y = Input.GetAxis("Vertical");
            dir = new Vector2(x, y);

            Walk(dir);
        }else if(!inputMobile){ // -----> PC input
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            dir = new Vector2(x, y);
            Walk(dir);
        }
    }

    public void ButtonDown(int side){
        btnSide = side;
        StartCoroutine(PressButton());

        if(btnSide == 0){
            if(coll.onGround){
                Jump(Vector2.up, false);
                jumping = true;
            }
            if((coll.onWall && !coll.onGround) || onLeaveWall){
                WallJump();
                onLeaveWall = false;
                onWall = false;
            }
        }
    }

    public void ButtonUp(){
        StopAllCoroutines();
        btnPress = false;
        jumpingSustain = false;
    }

    public IEnumerator PressButton(){
        while (true){
            yield return null;

            if(btnSide == 1){
                if(maxAxis < 1)
                    maxAxis = Mathf.MoveTowards(maxAxis, 1, 3 * Time.deltaTime);

                btnPress = true;
                x = maxAxis;
            }

            if(btnSide == -1){
                if(maxAxis > -1)
                    maxAxis = Mathf.MoveTowards(maxAxis, -1, 3 * Time.deltaTime);

                btnPress = true;
                x = maxAxis;
            }

            if(btnSide == 0){
                jumpingSustain = true;
            }
        }
    }

    IEnumerator SaiuDaParede(){
        onLeaveWall = true;
        yield return new WaitForSeconds(.15f);
        onLeaveWall = false;
    }
}