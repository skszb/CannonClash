using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class pirate : MonoBehaviour
{
    public float life;
    public float dir;
    private float curSpeed;

    // Start is called before the first frame update
    void Start()
    {
        life = 100;
    }

    public void damage(float d)
    {
        life -= d;
        if (life <= 0)
        {
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

        if (transform.position.x >= -100 && transform.position.x <= 100)
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
        Destroy(gameObject);
    }
}
