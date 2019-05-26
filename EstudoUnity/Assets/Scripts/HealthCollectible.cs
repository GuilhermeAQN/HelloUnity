using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        PlayerController playerController = other.GetComponent<PlayerController>();
        
        if(playerController != null){
            if(playerController.health < playerController.maxHealth){
                playerController.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}