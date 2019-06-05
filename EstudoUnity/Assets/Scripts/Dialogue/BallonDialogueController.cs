using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonDialogueController : MonoBehaviour{
    
    public static BallonDialogueController instance { get; set; }

    public Animator anim;
    
    void Awake() {
        instance = this;    
    }
    
    public void DisplayDialogue(){
        anim.SetBool("inDialogue", true);
    }
    
    public void EndDialogue(){
        anim.SetBool("inDialogue", false);
    }
}
