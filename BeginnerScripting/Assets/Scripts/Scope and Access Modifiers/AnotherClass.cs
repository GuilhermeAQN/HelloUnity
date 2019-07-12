using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherClass : MonoBehaviour{
    
    public int apples;
    public int bananas;

    private int stapler;
    private int sllotape;

    public void FruitMachine(int a, int b){
        int responda;
        responda = a + b;
        Debug.Log("Total de Frutas: " + responda);
    }

    private void OfficeSort(int a, int b){
        int responda;
        responda = a + b;
        Debug.Log("Total de material de escritório: " + responda);
    }
}
