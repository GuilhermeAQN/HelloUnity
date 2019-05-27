﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rb2D;
    float timer;
    int direction = 1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();     
        timer = changeTime;   
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0){
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rb2D.position;

        if(vertical){
            position.y = position.y + Time.deltaTime * speed * direction;
        }else{
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rb2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if(playerController != null){
            playerController.ChangeHealth(-1);
        }    
    }
}
