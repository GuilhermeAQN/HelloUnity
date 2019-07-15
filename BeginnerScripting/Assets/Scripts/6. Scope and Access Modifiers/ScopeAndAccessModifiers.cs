using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeAndAccessModifiers : MonoBehaviour{

    public int alpha = 5;

    private int beta = 0;
    private int gamma = 5;

    private AnotherClass anotherClass;

    void Start(){
        anotherClass = GetComponent<AnotherClass>();
        anotherClass.FruitMachine(alpha, anotherClass.apples);
    }
    
    void Example(int pens, int crayons){
        int answer;
        answer = pens + crayons;
        Debug.Log(answer);
    }

    void Update(){
        Debug.Log("Alpha esta definido para: " + alpha + beta + gamma);
    }
}
