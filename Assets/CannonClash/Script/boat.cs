using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    public float life;
    
    // Start is called before the first frame update
    void Start()
    {
        //default is 100, can also be set after spawn for pirate ships
        life = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLife(float l) {
        life = l;
    }

    public void damage(float d) {
        life -= d;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            // update life after hit by cannon ball
        }

        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            // pirate ship collides with player ship
            boat b = collision.gameObject.GetComponent<boat>();
            b.damage(10); // change after cannon ball and damage system is implemented
            Die();
        }
    }

    private void Die() { 
        Destroy(gameObject);
    }
}
