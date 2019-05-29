using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    Rigidbody2D rb2D;

    public int maxHealth = 5;

    public int health{ get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    public float timeInvincible = 2.0f;
    float invincibleTimer;

    Animator anim;
    Vector2 lookDirection = new Vector2(0,-1);

    void Start(){
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
    }

    void Update(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", move.magnitude);

        Vector2 position = rb2D.position;
        
        if(Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 2){
            position = position + move * speed / Mathf.Sqrt(2) * Time.deltaTime;
        }else{
            position = position + move * speed * Time.deltaTime;
        }
        Debug.Log(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        rb2D.MovePosition(position);

        if(isInvincible){
            invincibleTimer -=Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }
    }
    
    public void ChangeHealth(int amount){
        if(amount < 0){
            if(isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
