using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CogAmmoControll : MonoBehaviour{
    public static CogAmmoControll instence { get; private set; }
    
    public TextMeshProUGUI cogAmmoTxt;
    public int maxCogAmmo;
    [HideInInspector]
    public int currentCogAmmo;

    void Awake() {
        instence = this;
    }

    void Update() {
        cogAmmoTxt.text = currentCogAmmo.ToString() + " / " + maxCogAmmo.ToString();

        if(currentCogAmmo > maxCogAmmo){
            currentCogAmmo = maxCogAmmo;
        }
    }

    public void PickUpAmmo(int value){
        if(currentCogAmmo < maxCogAmmo){
            currentCogAmmo += value;
        }
    }
}
