using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherClass : MonoBehaviour{
    
    public int apples;
    public int bananas;

    private int stapler;
    private int sllotape;

    public void FruitMachine(int a, int b){
        int answer;
        answer = a + b;
        Debug.Log("Total de Frutas: " + answer);
    }

    private void OfficeSort(int a, int b){
        int answer;
        answer = a + b;
        Debug.Log("Total de material de escritório: " + answer);
    }
}
