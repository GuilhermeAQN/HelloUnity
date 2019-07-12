using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeAndAccessModifiers : MonoBehaviour{

    public int alpha = 5;

    private int beta = 0;
    private int gamma = 5;

    private AnotherClass myOtherClass;

    void Start(){
        alpha = 29;

        myOtherClass = new AnotherClass();
        myOtherClass.FruitMachine(alpha, myOtherClass.apples);
    }
    
    void Example(int pens, int crayons){
        int responda;
        responda = pens + crayons;
        Debug.Log(responda);
    }

    void Update(){
        Debug.Log("Alpha esta definido para: " + alpha);
    }
}
