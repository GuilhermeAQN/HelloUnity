﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogAmmoCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        PlayerController playerController = other.GetComponent<PlayerController>();

        if(playerController != null){
            if(CogAmmoControll.instence.currentCogAmmo < CogAmmoControll.instence.maxCogAmmo){
                CogAmmoControll.instence.PickUpAmmo(Random.Range(1, 5));
                Destroy(gameObject);
            }
        }

    }
}
