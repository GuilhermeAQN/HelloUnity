using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    public int btnSide;

    public void OnPointerDown(PointerEventData eventData){
        StartCoroutine(PressButton());
    }
    public void OnPointerUp(PointerEventData eventData){
        StopAllCoroutines();
    }

    IEnumerator PressButton(){
        while (true){
            yield return null;
            
            if(btnSide == 1)
                Debug.Log("Apertando Esquerdo");

            if(btnSide == -1)
                Debug.Log("Apertando Esquerdo");

        }
    }

    public void Move(float value){
        float x = value;
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        PlayerController.instence.Walk(dir);
    }
}