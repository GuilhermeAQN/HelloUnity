using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    public PlayerController player;
    public Collisions coll;

    public int btnSide;

    public float axisGravity = 3;
    public float axisSensitivity = 3;
    public float maxAxis = 0;

    public bool btnPress;
    public bool onWall;
    public bool onLeaveWall;

    void Update(){
        if(btnPress == false){
            if(maxAxis != 0){
                maxAxis = Mathf.MoveTowards(maxAxis, 0, axisGravity * Time.deltaTime);
                player.x = maxAxis;
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
            if(player.inputMobile == false){
                player.inputMobile = true;
                return;
            }else{
                player.inputMobile = false;
                return;
            }
        }
        if(player.inputMobile){  // -----> Mobile input
            player.y = Input.GetAxis("Vertical");
            player.dir = new Vector2(player.x, player.y);

            player.Walk(player.dir);
        }else if(!player.inputMobile){ // -----> PC input
            player.x = Input.GetAxis("Horizontal");
            player.y = Input.GetAxis("Vertical");
            player.dir = new Vector2(player.x, player.y);
            player.Walk(player.dir);
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        StartCoroutine(PressButton());

        if(btnSide == 0){
            if(coll.onGround){
                player.Jump(Vector2.up, false);
                player.jumping = true;
            }
            if((coll.onWall && !coll.onGround) || onLeaveWall){
                player.WallJump();
                onLeaveWall = false;
                onWall = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData){
        StopAllCoroutines();
        btnPress = false;
        player.jumpingSustain = false;
    }

    IEnumerator PressButton(){
        while (true){
            yield return null;

            if(btnSide == 1){
                if(maxAxis < 1)
                    maxAxis = Mathf.MoveTowards(maxAxis, 1, axisSensitivity * Time.deltaTime);

                btnPress = true;
                player.x = maxAxis;
            }

            if(btnSide == -1){
                if(maxAxis > -1)
                    maxAxis = Mathf.MoveTowards(maxAxis, -1, axisSensitivity * Time.deltaTime);

                btnPress = true;
                player.x = maxAxis;
            }

            if(btnSide == 0){
                player.jumpingSustain = true;
            }
        }
    }

    IEnumerator SaiuDaParede(){
        onLeaveWall = true;
        yield return new WaitForSeconds(.15f);
        onLeaveWall = false;
    }
}