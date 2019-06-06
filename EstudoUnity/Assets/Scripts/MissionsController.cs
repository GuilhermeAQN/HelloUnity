using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionsController : MonoBehaviour{

    public static MissionsController instance { get; private set; }

    int enemiesCount;
    public List<GameObject> go;
    TextMeshProUGUI text;

    void Awake(){
        instance = this;
        go = new List<GameObject>(GameObject.FindGameObjectsWithTag("Robots"));
        enemiesCount = go.Count;
    }

    void Update(){
        
    }
}