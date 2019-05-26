using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;

    public int health{ get { return currentHealth; } }
    int currentHealth;

    public float speed = 0.1f;
    Rigidbody2D rb2D;

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
    }
    
    public void ChangeHealth(int amount){
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
