using UnityEngine;

public class WhileLoop : MonoBehaviour{
    
    int cupsInTheSink = 4;

    void Start(){
        while (cupsInTheSink > 0){
            Debug.Log("Eu lavei o copo!");
            cupsInTheSink--;
        }
    }
}
