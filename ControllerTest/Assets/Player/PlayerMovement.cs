using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    
    private Animations anim;
    [HideInInspector]
    public Collisions coll;
    [HideInInspector]
    public Rigidbody2D rb;
    
    [Header("Movement Properties")]
    public float speed = 7;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Jump Properties")]
    public float jumpForce = 5;

    [Header("Wall Properties")]
    public float slideSpeed = 2;
    public float wallJumpLerp = 10;

    [Header("Status")]
    public bool canMove;
    public bool wallJumped;
    public bool wallSlide;
    public bool jumping;
    public bool jumpingSustain;

    [Header("Directions")]
    public float x;
    public float y;
    public float dirX = 1;
    public Vector2 dir;

    void Awake(){
        coll = GetComponent<Collisions>();
        anim = GetComponent<Animations>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // --------- Update Directions --------- \\
        if(x > 0)
            dirX = 1;
        else if(x < 0)
            dirX = -1;

        // -------------------- Call Wall Slide -------------------- \\
        if(coll.onWall && !coll.onGround){
            if(x != 0 && !jumping){
                wallSlide = true;
                WallSlide();
            }else if(x <= .5f || x >= -.5f){
                wallSlide = false;
            }
            if(x != 0 && jumping){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if(coll.onGround){
            wallSlide = false;
            wallJumped = false;
        }

        // -------------------- Jump Sustain Controller --------------------\\
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            jumping = false;
        }
        else if(rb.velocity.y > 0 && !jumpingSustain){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Walk(Vector2 dir){
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
        anim.Flip(dirX);
    }

    public void Jump(Vector2 dir, bool wall){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
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

    public void WallJump(){
        if(!jumping){
            if((dirX == 1 && coll.onRightWall) || dirX == -1 && coll.onLeftWall){
                dirX *= -1;
                anim.Flip(dirX);
            }

            StopCoroutine(DisableMovement(0));
            StartCoroutine(DisableMovement(.15f));

            Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

            Jump((Vector2.up / 1.3f + wallDir / 1.5f), true);

            wallJumped = true;
        }
    }

    IEnumerator DisableMovement(float time){
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
