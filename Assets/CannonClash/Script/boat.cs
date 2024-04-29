using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    public float life;
    public int ammo;
    
    // Start is called before the first frame update
    void Start()
    {
        life = 100.0f;
        ammo = 0;
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
        if (life <= 0) 
        {
            // game over and die
            Die();
        }
    }

    private void Die() { 
        Destroy(gameObject);
    }
}
