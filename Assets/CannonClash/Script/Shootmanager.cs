using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootmanager : MonoBehaviour
{
    [SerializeField] GameObject[] cannons;

   
    private float shootInterval = 5f;
  
    private float timeSinceLastShot = 0f;
    void Start()
    {
        if (cannons.Length == 0)
        {
            cannons = GameObject.FindGameObjectsWithTag("cannon");
        }
    }

    // Update is called once per frame
    void Update()
    {
     
        timeSinceLastShot += Time.deltaTime;

        
        if (timeSinceLastShot >= shootInterval)
        {
            if (cannons.Length > 0)
            {
                int randomIndex = Random.Range(0, cannons.Length);
                ShootCannon(cannons[randomIndex]); 
            }

            
            timeSinceLastShot = 0f;
        }
    }
    void ShootCannon(GameObject cannon)
    {
     
        Cannoncontroller controller = cannon.GetComponent<Cannoncontroller>();
        if (controller != null)
        {
            controller.shoot();
        }
    }

   public void setshootpower(float power)
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            GameObject cannon = cannons[i];
            Cannoncontroller controller = cannon.GetComponent<Cannoncontroller>();
            controller.Blastpower = power;
        }
    }
}
