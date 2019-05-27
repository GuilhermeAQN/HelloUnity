using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other){
        PlayerController playerController = other.GetComponent<PlayerController>();

        if(playerController != null){
            playerController.ChangeHealth(-1);
        }
    }
}
