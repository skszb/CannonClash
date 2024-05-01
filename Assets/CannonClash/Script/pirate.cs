using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class pirate : MonoBehaviour
{
    private float life;
    public float dir;
    public int row;
    private float curSpeed;
    public GameObject finalExplosionEffect;
    Global g;

    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        g = GameObject.Find("Global").GetComponent<Global>();
    }

    public void damage(float d)
    {
        life -= d;
        if (life <= 0)
        {
            g.increScore();
            GameObject obj = Instantiate(finalExplosionEffect, transform.position + new Vector3(0f, 2.5f, 0f), Quaternion.identity) as GameObject;
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -255f || transform.position.x > 255f) 
        {
            Die();
        }

        if (transform.position.x >= -85 && transform.position.x <= 85)
        {
            curSpeed = 3 * dir;
        }
        else 
        {
            curSpeed = 12 * dir; ;
        }
    }

    private void FixedUpdate()
    {
        Vector3 newPos = transform.position + new Vector3(curSpeed * Time.deltaTime, 0f, 0f);
        transform.position = newPos;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("PlayerShip"))
        {
            // pirate ship collides with player ship
            boat b = collision.gameObject.GetComponent<boat>();
            b.damage(20); // change after cannon ball and damage system is implemented
            Die();
        }
    }

    private void Die()
    {
        if (dir == 1f)
        {
            g.leftShipDestroy(row);
        }
        else 
        {
            g.rightShipDestroy(row);
        }
        Destroy(gameObject);
    }
}
