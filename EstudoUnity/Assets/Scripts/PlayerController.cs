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
    
    public GameObject projectilePrefab;

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
        rb2D.MovePosition(position);

        if(isInvincible){
            invincibleTimer -=Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C)){
            Launch();
        }

        //Dialogue with NPC----
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f,lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if(Input.GetKeyDown(KeyCode.X)){
            if(hit.collider != null){
                JambiController.instance.DisplayDialogue();
            }
        }
        //-----
    }
    
    public void ChangeHealth(int amount){
        if(amount < 0){
            if(isInvincible)
                return;

            anim.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float) maxHealth);
    }

    
    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        anim.SetTrigger("Launch");
    }
}
