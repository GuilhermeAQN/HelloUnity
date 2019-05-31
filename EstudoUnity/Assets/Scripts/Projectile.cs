using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2D;
    public ParticleSystem atkParticle;

    void Awake(){
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(transform.position.magnitude > 50.0f){
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force){
        rb2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemieController e = other.gameObject.GetComponent<EnemieController>();
        if(e != null){
            e.Fix();
        }
        Instantiate(atkParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
