using System.Collections;
using UnityEngine;

public class ForLoop : MonoBehaviour{
    
    int numEnemies = 3;

    void Start(){
        for (int i = 0; i < numEnemies; i++){
            Debug.Log("Criando numero de inimigos: " + i);
        }
    }
}
