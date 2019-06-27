using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieAnimControll : MonoBehaviour
{
    Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }
}
