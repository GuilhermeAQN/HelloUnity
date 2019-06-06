using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instence { get; private set; }

    //public variables
    public float speed = 0.1f;
    public float timeInvincible = 2.0f;
    public float timeCantWalk;
    public int maxHealth = 10;
    public int health{ get { return currentHealth; } }
    public GameObject projectilePrefab;
    [HideInInspector]
    public bool talkingNPC;

    //private variables
    int currentHealth;
    float invincibleTimer;
    float cantWalkTimer;
    bool canWalk;
    bool isInvincible;
    Rigidbody2D rb2D;
    Animator anim;
    Vector2 lookDirection = new Vector2(0,-1);


    void Start(){
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        instence = this;
        canWalk = true;
    }

    void Update(){
        if(!talkingNPC && canWalk){
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

            if(Input.GetKeyDown(KeyCode.C)){
                LaunchObject();
            }
        }

        if(isInvincible){
            invincibleTimer -=Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }

        if(!canWalk){
            cantWalkTimer -=Time.deltaTime;
            if(cantWalkTimer < 0)
                canWalk = true;
        }

        //Dialogue with NPC----
        RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f,lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if(Input.GetKeyDown(KeyCode.X) && talkingNPC == false){
            if(hit.collider != null){
                talkingNPC = true;
                BallonDialogueController.instance.DisplayDialogue();
                DialogueTrigger.instence.TriggerDialogue();
            }
        }else if(Input.GetKeyDown(KeyCode.X) && talkingNPC == true){
            DialogueManager.instence.DisplayNextSentence();
        }
        //---------------
    }
    
    public void ChangeHealth(int amount){
        if(amount < 0){
            if(isInvincible)
                return;

            anim.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
            canWalk = false;
            cantWalkTimer = timeCantWalk;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float) maxHealth);
    }

    
    void LaunchObject(){
        if(CogAmmoControll.instence.currentCogAmmo > 0 && canWalk){
            GameObject projectileObject = Instantiate(projectilePrefab, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);
            anim.SetTrigger("Launch");
            CogAmmoControll.instence.currentCogAmmo--;
            canWalk = false;
            cantWalkTimer = timeCantWalk;
        }
    }
}
