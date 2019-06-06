using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour{

    public static DialogueManager instence { get; private set; }
    [Header ("Nome do NPC")]
    public TextMeshProUGUI nameText;
    [Header ("Caixa de dialogo")]
    public TextMeshProUGUI dialogueText;

    [Header ("DialogueBox")]
    public Animator dialogueCanvas;

    private Queue<string> sentences;
    bool b_proximoText = false;
    string s_proximoText;
    
    void Awake(){
        DialogueManager.instence = this;
    }

    void Start(){
        sentences = new Queue<string>();
    }

    void Update() {
        if(b_proximoText == true){
            dialogueText.text += s_proximoText[0];
            s_proximoText = s_proximoText.Substring(1);
            if(s_proximoText.Length == 0){
                b_proximoText = false;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue){
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        dialogueCanvas.SetBool("Open", true);
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        s_proximoText = sentence;
        b_proximoText = true;
        dialogueText.text = string.Empty;
    }

    void EndDialogue(){
        PlayerController.instence.talkingNPC = false;
        BallonDialogueController.instance.anim.SetBool("inDialogue", false);
        dialogueCanvas.SetBool("Open", false);
    }
}