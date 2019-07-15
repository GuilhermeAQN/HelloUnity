using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour{

    public Light myLight;

    // In this case, result = 4
    float fResult = Mathf.Lerp(3f, 5f, 0.5f);

    Vector3 from = new Vector3(1f, 2f, 3f);
    Vector3 to = new Vector3(5f, 6f, 7f);

    void Start(){
        myLight = GetComponent<Light>();
        Debug.Log("Lerp resultado: " + fResult);
        // Here result = (4, 5, 6)
        Vector3 result = Vector3.Lerp (from, to, 0.75f);
        Debug.Log("Vector3 Lerp resultado: " + result);
    }

    void Update() {
        myLight.intensity = Mathf.Lerp(myLight.intensity, 8f, 0.5f * Time.deltaTime);
    }
    
}
