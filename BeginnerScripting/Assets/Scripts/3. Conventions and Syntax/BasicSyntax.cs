using UnityEngine;

public class BasicSyntax : MonoBehaviour{
    void Start(){
        Debug.Log(transform.position.x);

        if(transform.position.y <= 5f){
            Debug.Log("Estou prestes a bater no chão");
        }
    }
}
