using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerWalk : MonoBehaviour{

    PlayerManager pM;
    Animations anim;

    public float speed;

    void Awake(){
        pM = GetComponent<PlayerManager>();
    }

    public void Walk(Vector2 dir){
        if(!pM.canMove)
            return;

        if(pM.coll.onWall){
            pM.rb.velocity = new Vector2(pM.rb.velocity.x, pM.rb.velocity.y);
            return;
        }

        if(!pM.wallJumped){
            pM.rb.velocity = new Vector2(dir.x * speed, pM.rb.velocity.y);
        }else{
            pM.rb.velocity = Vector2.Lerp(pM.rb.velocity, (new Vector2(dir.x * speed, pM.rb.velocity.y)), 10 * Time.deltaTime);
        }
        anim.Flip(pM.dirX);
    }
}
