using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CannonBall : MonoBehaviour
{
    // private XRGrabInteractable _grabInteractable;

    public bool catchable = true; // the b
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            // Hit Player Ship
            boat b = collision.gameObject.GetComponent<boat>();
            b.damage(5);
            Die();
        }

        if (collision.gameObject.CompareTag("PirateMinion"))
        {
            // Hit Pirate Minion
            pirate b = collision.gameObject.GetComponent<pirate>();
            b.damage(50);
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15f) 
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
