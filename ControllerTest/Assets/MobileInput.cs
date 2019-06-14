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

    void Update(){
        if(btnPress == false){
            if(maxAxis != 0){
                maxAxis = Mathf.MoveTowards(maxAxis, 0, axisGravity * Time.deltaTime);
                Move(maxAxis);
            }
            // if(Mathf.Abs(maxAxis) < 0.05){
            //     maxAxis = 0;
            // }
        }
        if(Input.GetKeyDown(KeyCode.C)){
            if(player.inputMobile == false){
                player.inputMobile = true;
                return;
            }

            if(player.inputMobile == true){
                player.inputMobile = false;
                return;
            }
        }

        if(!player.inputMobile){
            player.x = Input.GetAxis("Horizontal");
            player.y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(player.x, player.y);
            player.Walk(dir);
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        StartCoroutine(PressButton());
    }
    public void OnPointerUp(PointerEventData eventData){
        StopAllCoroutines();
        btnPress = false;
    }

    IEnumerator PressButton(){
        while (true){
            yield return null;
            
            if(btnSide == 1){
                if(maxAxis < 1)
                    maxAxis = Mathf.MoveTowards(maxAxis, 1, axisSensitivity * Time.deltaTime);

                btnPress = true;
                Move(maxAxis);
            }

            if(btnSide == -1){
                if(maxAxis > -1)
                    maxAxis = Mathf.MoveTowards(maxAxis, -1, axisSensitivity * Time.deltaTime);

                btnPress = true;
                Move(maxAxis);
            }

            if(btnSide == 2){
                Debug.Log("Apertando Pulo");
                if(coll.onGround){
                    player.Jump(Vector2.up, false);
                    player.jumping = true;
                }
                if(coll.onWall && !coll.onGround)
                    player.WallJump();
            }
        }
    }

    public void Move(float value){
        if(player.inputMobile){
            player.x = value;
            player.y = Input.GetAxis("Vertical");
            Vector2 dir = new Vector2(player.x, player.y);

            player.Walk(dir);
        }
        
    }
}