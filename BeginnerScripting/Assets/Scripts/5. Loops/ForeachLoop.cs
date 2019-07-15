using System.Collections;
using UnityEngine;

public class ForeachLoop : MonoBehaviour{

    void Start(){
        string[] strings = new string[3];

        strings[0] = "Primeira string";
        strings[1] = "Segunda string";
        strings[2] = "Terceira string";

        foreach (string item in strings){
            print(item);
        }
    }    
}
