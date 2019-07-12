using UnityEngine;

public class WhileLoop : MonoBehaviour{
    
    int copoNaPia = 4;

    void Start(){
        while (copoNaPia > 0){
            Debug.Log("Eu lavei o copo!");
            copoNaPia--;
        }
    }
}
