using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStack : MonoBehaviour
{
    [SerializeField] private boat m_Boat = null;
    // Start is called before the first frame update
    void Start()
    {
        if (m_Boat == null)
        {
            m_Boat = gameObject.GetComponentInParent<boat>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CannonBall"))
        {
            other.gameObject.GetComponent<CannonBall>().Die();
            m_Boat.increAmmo();
        }
    }
}
