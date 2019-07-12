using System.Collections;
using UnityEngine;

public class ForLoop : MonoBehaviour{
    
    int numInimigos = 3;

    void Start(){
        for (int i = 0; i < numInimigos; i++){
            Debug.Log("Criando numero de inimigos: " + i);
        }
    }
}
