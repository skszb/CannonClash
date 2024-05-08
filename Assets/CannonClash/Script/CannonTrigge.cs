using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonTrigge : MonoBehaviour
{
    private Collider m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cannon"))
        {
            IEnumerator c = DisableFor(3.0f);
            StartCoroutine(c);
        }
    }

    IEnumerator DisableFor(float time)
    {
        m_collider.enabled = false;
        yield return new WaitForSeconds(time);
        m_collider.enabled = true;
    }
}
