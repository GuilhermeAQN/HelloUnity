using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;

    Rigidbody2D rb2D;
    float timer;
    int direction = 1;

    Animator anim;
    bool broken = true;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();     
        timer = changeTime;   
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!broken){
            anim.SetTrigger("Dead");
            return;
        }

        timer -= Time.deltaTime;

        if(timer < 0){
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rb2D.position;

        if(vertical){
            position.y = position.y + Time.deltaTime * speed * direction;
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", direction);
        }else{
            position.x = position.x + Time.deltaTime * speed * direction;
            anim.SetFloat("MoveX", direction);
            anim.SetFloat("MoveY", 0);
        }
        rb2D.MovePosition(position);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if(playerController != null){
            playerController.ChangeHealth(-1);
        }
    }

    public void Fix(){
        broken = false;
        rb2D.simulated = false;
        smokeEffect.Stop();
    }
}
