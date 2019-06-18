using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onPointerDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    
    public pointerDown pointer;
    public PlayerInput input;

    public void OnPointerDown(PointerEventData eventData){
        if(pointer == pointerDown.LEFT)
            input.ButtonDown(-1);

        if(pointer == pointerDown.RIGHT)
            input.ButtonDown(1);
            
        if(pointer == pointerDown.JUMP)
            input.ButtonDown(0);
    }

    public void OnPointerUp(PointerEventData eventData){
        if(pointer == pointerDown.LEFT)
            input.ButtonUp();

        if(pointer == pointerDown.RIGHT)
            input.ButtonUp();
            
        if(pointer == pointerDown.JUMP)
            input.ButtonUp();
    }
}
public enum pointerDown{
    LEFT,
    RIGHT,
    JUMP
}
