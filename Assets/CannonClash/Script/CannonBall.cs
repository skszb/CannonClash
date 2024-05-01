using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CannonBall : MonoBehaviour
{
    // private XRGrabInteractable _grabInteractable;

    public bool catchable = true; // the b
    public AudioClip strikeSound;
    public GameObject pirateOnHitEffect;
    public GameObject playerOnHitEffect;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            // Hit Player Ship
            // adjust audio source location and on-hit explosion spawn location accordingly
            AudioSource.PlayClipAtPoint(strikeSound, new Vector3(0f, -5f, 0f));
            GameObject obj = Instantiate(playerOnHitEffect, collision.contacts[0].point, Quaternion.identity) as GameObject;
            boat b = collision.gameObject.GetComponent<boat>();
            b.damage(5);
            Die();
        }

        if (collision.gameObject.CompareTag("PirateMinion"))
        {
            // Hit Pirate Minion
            // adjust audio source location and on-hit explosion spawn location accordingly
            AudioSource.PlayClipAtPoint(strikeSound, new Vector3(0f, 5f, 15f));
            GameObject obj = Instantiate(pirateOnHitEffect, collision.contacts[0].point, Quaternion.identity) as GameObject;
            pirate p = collision.gameObject.GetComponent<pirate>();
            p.damage(50);
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
