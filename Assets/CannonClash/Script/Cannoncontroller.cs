using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannoncontroller : MonoBehaviour
{
    public GameObject Cannonball;
    public Transform Shotpoint;
    public float Blastpower = 40;

    /*float timeinterval = 5;
    private void Update()
    {
        timeinterval -= 1f * Time.deltaTime;
        if(timeinterval <= 0)
        {
            shoot();
            timeinterval = 5;
        }
    }*/


    public void shoot()
    {
        GameObject createcannonball = Instantiate(Cannonball, Shotpoint.position,Shotpoint.rotation);
        createcannonball.GetComponent<Rigidbody>().velocity = Shotpoint.transform.up * Blastpower;
    }

}
