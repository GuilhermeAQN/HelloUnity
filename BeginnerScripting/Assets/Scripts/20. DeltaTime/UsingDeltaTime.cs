using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingDeltaTime : MonoBehaviour
{
    public float speed = 8f;
    public float countdown = 3.0f;

    void Update(){
        countdown -= Time.deltaTime;
        if(countdown <= 0.0f)
            GetComponent<Light>().enabled = true;

        if(Input.GetKey(KeyCode.RightArrow))
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f,0.0f);
        if(Input.GetKey(KeyCode.LeftArrow))
            transform.position += new Vector3(-speed * Time.deltaTime, 0.0f,0.0f);
    }
}
