using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyInput : MonoBehaviour{

    Image img;

    public TextMeshProUGUI text;

    public Color standard;
    public Color downgfx;
    public Color upgfx;
    public Color heldgfx;

    private void Start() {
        img = GetComponent<Image>();
    }
    
    private void Update(){
        bool down = Input.GetKeyDown(KeyCode.Space);
        bool held = Input.GetKey(KeyCode.Space);
        bool up = Input.GetKeyUp(KeyCode.Space);

        if(down){
            img.color = downgfx;
            Debug.Log("Down");
        }
        else if(held){
            img.color = heldgfx;
            Debug.Log("Held");
        }
        else if(up){
            img.color = upgfx;
            Debug.Log("Up");
        }
        else{
            img.color = standard;
        }

        text.text = down + "\n\n" + held + "\n\n" + up;
    }
}
