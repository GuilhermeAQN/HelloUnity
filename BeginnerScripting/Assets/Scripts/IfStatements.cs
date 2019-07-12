using System.Collections;
using UnityEngine;

public class IfStatements : MonoBehaviour{

    float coffeTemperature = 85.0f;
    float hotLimitTemperature = 70.0f;
    float coldLimitTemperature = 40.0f;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space))
            TemperatureTest();

        coffeTemperature -= Time.deltaTime * 5f;
    }

    void TemperatureTest(){
        // Se a temperatura do café é maior do que a temperatura mais quente para beber ...
        if(coffeTemperature > hotLimitTemperature){
            //... Faça isso.
            print("O café esta muito quente.");
        }
        // Se não for, mas a temperatura do café é inferior à temperatura de bebida mais baixa...
        else if(coffeTemperature < coldLimitTemperature){
            // ... Faça isso.
            print("O café esta muito frio.");
        }
        // Se não for nem um deles, então ...
        else{
            // ... Faça isso.
            print("Café é está bom.");
        }
    }
}
