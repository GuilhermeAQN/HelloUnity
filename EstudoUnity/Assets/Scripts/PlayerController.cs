using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public float speed = 0.1f;
    public float timeInvincible = 2.0f;
    public int maxHealth = 5;
    public int health{ get { return currentHealth; } }
    public GameObject projectilePrefab;

    //private variables
    int currentHealth;
    float invincibleTimer;
    bool inDialogue;
    bool isInvincible;
    Rigidbody2D rb2D;
    Animator anim;
    Vector2 lookDirection = new Vector2(0,-1);


    void Start(){
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(inDialogue == false){
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
                LaunchObject();
            }
        }

        //Dialogue with NPC----
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f,lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if(Input.GetKeyDown(KeyCode.X) && inDialogue == false){
            if(hit.collider != null){
                inDialogue = true;
                JambiController.instance.DisplayDialogue();
                DialogueTrigger.instence.TriggerDialogue();
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

    
    void LaunchObject(){
        GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        anim.SetTrigger("Launch");
    }
}
