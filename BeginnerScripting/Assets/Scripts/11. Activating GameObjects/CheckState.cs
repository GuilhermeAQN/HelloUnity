using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour{

    public GameObject myObject;

    void Start(){
        Debug.Log("Ativar-se: " + myObject.activeSelf);
        Debug.Log("Ativar na Hierarquia" + myObject.activeInHierarchy);
    }

}
