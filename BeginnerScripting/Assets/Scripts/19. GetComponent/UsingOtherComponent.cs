using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingOtherComponent : MonoBehaviour{
    
    public GameObject otherGameObject;

    private AnotherScript anotherScript;
    private YetAnotherScript yetAnotherScript;
    private BoxCollider boxColl;

    private void Awake() {
        anotherScript = GetComponent<AnotherScript>();
        yetAnotherScript = otherGameObject.GetComponent<YetAnotherScript>();
        boxColl = otherGameObject.GetComponent<BoxCollider>();
    }

    void Start() {
        boxColl.size = new Vector3(3,3,3);
        Debug.Log("The player's score is " + anotherScript.playerScore);
        Debug.Log("The player has died " + yetAnotherScript.numberOfPlayerDeaths + " times");    
    }

}
