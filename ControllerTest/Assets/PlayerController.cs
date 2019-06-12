using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private Animations anim;
    private Collisions coll;
    [HideInInspector]
    public Rigidbody2D rb;

    public float speed = 10;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float slideSpeed = 2;
    public float wallJumpLerp = 10;
    
    [HideInInspector]
    public float dirX = 1;

    public bool canMove;
    public bool wallJumped;
    public bool wallSlide;
    public bool jumping;

    private Vector2 dir;

    void Start(){
        coll = GetComponent<Collisions>();
        anim = GetComponent<Animations>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        if(x > 0)
            dirX = 1;
        else if(x < 0)
            dirX = -1;

        Walk(dir);

        //----------Call Wall Slide----------//
        if(coll.onWall && !coll.onGround){
            if(x != 0 && !jumping){
                wallSlide = true;
                WallSlide();
            }
            if(x != 0 && jumping){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if(coll.onGround){
            wallSlide = false;
            wallJumped = false;
        }

        //----------Call Jump and Wall Jump----------
        if(Input.GetButtonDown("Jump")){
            // anim.SetTrigger("jump");

            if(coll.onGround){
                Jump(Vector2.up, false);
                jumping = true;
            }
            if(coll.onWall && !coll.onGround)
                WallJump();
        }

        //----------Jump Sustain Code----------//
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (rb.velocity.y < 0)
            jumping = false;
    }

    void Walk(Vector2 dir){
        if(!canMove)
            return;

        if(coll.onWall){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            return;
        }

        if(!wallJumped){
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }else{
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    void Jump(Vector2 dir, bool wall){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void WallJump(){
        if((dirX == 1 && coll.onRightWall) || dirX == -1 && coll.onLeftWall){
            dirX *= -1;
            anim.Flip(dirX);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.15f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.3f + wallDir / 1.3f), true);

        wallJumped = true;
    }


    void WallSlide(){
        if(coll.wallSide != dirX)
            anim.Flip(dirX * -1);
        
        if(!canMove)
            return;
        
        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall) && !jumping){
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        if(!jumping)
            rb.velocity = new Vector2(push, -slideSpeed);
    }

    IEnumerator DisableMovement(float time){
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
