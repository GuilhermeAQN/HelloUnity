using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Collisions : MonoBehaviour{

    public LayerMask groundMask;

    private BoxCollider2D coll;
    private PlayerController player;
    private RaycastOrigin raycastOrigin;

    float rayDis = 0.05f;

    const float skinWidth = .015f;
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    
    public int wallSide;

    void Awake(){
        coll = GetComponent<BoxCollider2D>();
        player = GetComponent<PlayerController>();
    }

    void FixedUpdate(){
        UpdateRayOrigin();
        UpdateCollision();
    }

    void UpdateCollision(){

    //--------------Horizontal Collisions--------------//
        float direction = player.dirX;

        Vector2 rayTopOrigin = (direction == -1)? raycastOrigin.topLeft : raycastOrigin.topRight;
        Vector2 rayBotOrigin = (direction == -1)? raycastOrigin.bottomLeft : raycastOrigin.bottomRight;

        Vector2 rayDirection = (direction == -1)? Vector2.right : Vector2.left;
        
        RaycastHit2D topMaxRay = Physics2D.Raycast(rayTopOrigin, rayDirection * -1, rayDis, groundMask);
        RaycastHit2D topMinRay = Physics2D.Raycast(rayBotOrigin, rayDirection * -1, rayDis, groundMask);

        // Debug.DrawRay(rayTopOrigin, rayDirection * -rayDis, Color.red);
        // Debug.DrawRay(rayBotOrigin, rayDirection * -rayDis, Color.red);

        if(topMaxRay || topMinRay)
            onWall = true;
        else
            onWall = false;

        if(direction == 1 && onWall)
            onRightWall = true;
        else
            onRightWall = false;

        if(direction == -1 && onWall)
            onLeftWall = true;
        else
            onLeftWall = false;

        wallSide = onRightWall ? -1 : 1;

    //--------------Vertical Collisions--------------//
        RaycastHit2D botLeftRay = Physics2D.Raycast(raycastOrigin.bottomLeft, Vector2.up * -1, rayDis, groundMask);
        RaycastHit2D botRightRay = Physics2D.Raycast(raycastOrigin.bottomRight, Vector2.up * -1, rayDis, groundMask);

        // Debug.DrawRay(raycastOrigin.bottomLeft, Vector2.up * -rayDis, Color.red);
        // Debug.DrawRay(raycastOrigin.bottomRight, Vector2.up * -rayDis, Color.red);
            
        if(botLeftRay || botRightRay){
            onGround = true;
        }else{
            onGround = false;
        }
    }

    public void UpdateRayOrigin(){
        Bounds bounds = coll.bounds;
        bounds.Expand (skinWidth * -.5f);
        
        raycastOrigin.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
        raycastOrigin.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
        raycastOrigin.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
        raycastOrigin.topRight = new Vector2 (bounds.max.x, bounds.max.y);
    }

    struct RaycastOrigin{
        public Vector2 bottomLeft, bottomRight;
        public Vector2 topLeft, topRight;
    }
}