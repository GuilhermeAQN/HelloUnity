using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour{
    
    public static DialogueTrigger instence { get; private set; }
    public Dialogue _dialogue;

    void Awake() {
        DialogueTrigger.instence = this;
    }

    public void TriggerDialogue(){
        DialogueManager.instence.StartDialogue(_dialogue);
    }
}