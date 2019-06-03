using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiController : MonoBehaviour{
    
    public static JambiController instance { get; set; }

    //public variables
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public Animator anim;
    //private variables
    private float timerDisplay;
    
    
    void Awake() {
        instance = this;    
    }
    
    void Start(){
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    private void Update() {
        if (timerDisplay >= 0){
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0){
                dialogBox.SetActive(false);
                anim.SetBool("inDialogue", false);
            }
        }
    }

    public void DisplayDialogue(){
        anim.SetBool("inDialogue", true);
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
