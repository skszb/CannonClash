using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] cannons;

   
    private float shootInterval = 5f;
  
    private float timeSinceLastShot = 0f;
    void Start()
    {
        cannons = GameObject.FindGameObjectsWithTag("cannon");
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
}
