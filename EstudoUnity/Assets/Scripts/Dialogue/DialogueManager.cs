using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour{

    public static DialogueManager instence { get; private set; }
    private Queue<string> sentences;

    void Awake(){
        DialogueManager.instence = this;
    }

    void Start(){
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
    }
}
