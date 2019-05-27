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

    void Start(){
        rb2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    void Update(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;

        if(Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 2){
            position.x = position.x + speed / Mathf.Sqrt(2) * horizontal * Time.deltaTime;
            position.y = position.y + speed / Mathf.Sqrt(2) * vertical * Time.deltaTime;
        }else{
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
        }
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
