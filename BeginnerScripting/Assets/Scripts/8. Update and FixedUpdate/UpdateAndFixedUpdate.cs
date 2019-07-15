using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAndFixedUpdate : MonoBehaviour{
    
    void FixedUpdate()
        // Chamado a cada etapa de física
        // Intervalos do FixedUpdate são consistentes
        // Usado para atualizações regulares, como:
        // Ajustando objetos com física (Rigidbody)
    {
        Debug.Log("Tempo do FixedUpdate: " + Time.deltaTime);
    }

    void Update()
        // Chamado todos os frames
        // Usado para atualizações regulares, como:
        // Mover objetos sem fisica
        // Temporizadores simples
        // Receber entradas

        // Os tempos de intervalo de atualização variam
    {
        Debug.Log("Tempo do Update: " + Time.deltaTime);
    }
}
