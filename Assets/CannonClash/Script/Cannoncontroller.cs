using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannoncontroller : MonoBehaviour
{
    public GameObject Cannonball;
    public Transform Shotpoint;
    public float Blastpower = 40;
    public AudioClip cannonShootSound;
    public GameObject shootingSmokeEffect;

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
        // adjust audio source location and smoke effect spawn location accordingly
        AudioSource.PlayClipAtPoint(cannonShootSound, new Vector3(0f, 10f, 30f));
        GameObject obj = Instantiate(shootingSmokeEffect, Shotpoint.position, Quaternion.identity) as GameObject;
        GameObject createcannonball = Instantiate(Cannonball, Shotpoint.position, Shotpoint.rotation);
        createcannonball.GetComponent<Rigidbody>().velocity = Shotpoint.transform.up * Blastpower;
    }

}
