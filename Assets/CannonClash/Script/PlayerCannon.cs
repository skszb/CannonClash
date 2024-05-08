using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    public GameObject Cannonball;
    public Transform Shotpoint;
    [SerializeField] float Blastpower = 40;
    [SerializeField] AudioSource cannonAudioSource;
    [SerializeField] ParticleSystem m_ShootingSmokeEffect;
    private ParticleSystem m_smokeParticleSystem;
    [SerializeField] private boat m_Boat;
    [SerializeField] private Handle m_handle;

    private bool ammoLimit = false;
    public void Start()
    {
        if (m_smokeParticleSystem == null)
        {
            m_smokeParticleSystem = Instantiate(m_ShootingSmokeEffect, Shotpoint.position, Quaternion.identity);
            m_smokeParticleSystem.Stop();
        }
        var psMain = m_smokeParticleSystem.main; 
        psMain.stopAction = ParticleSystemStopAction.None;
        
        if (m_Boat == null)
        {
            m_Boat = gameObject.GetComponentInParent<boat>();
        }
        
        if (m_Boat != null)
        {
            ammoLimit = true;
        }
        
        if (cannonAudioSource == null)
        {
            cannonAudioSource = GetComponent<AudioSource>();
        }

        if (m_handle == null)
        {
            m_handle = GetComponentInChildren<Handle>();
        }
    }


    public void Shoot()
    {
        // adjust audio source location and smoke effect spawn location accordingly
        if (ammoLimit && m_Boat.AmmoCount() <= 0)
        {
            return;
        }
        cannonAudioSource.Play();
        m_smokeParticleSystem.Play();
        GameObject createcannonball = Instantiate(Cannonball, Shotpoint.position, Shotpoint.rotation);
        createcannonball.GetComponent<CannonBall>().Catch();
        float power = Blastpower * m_handle.GetPowerRatio();
        createcannonball.GetComponent<Rigidbody>().velocity = Shotpoint.transform.up * power;
        Debug.Log(power);
        if (ammoLimit)
        {
            m_Boat.decreAmmo();
        }
    }

}
